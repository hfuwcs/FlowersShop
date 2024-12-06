using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;
using FlowersShop.ApiControllers;
using Newtonsoft.Json;
using System.Configuration;
using static FlowersShop.Models.Payments.VNPay.VnPayLibarry;

namespace FlowersShop.Controllers
{
    public class OrderController : Controller
    {
        QL_BanHoaEntities db = new QL_BanHoaEntities();

        public IList<Cart> GetCart()
        {
            if (Session["Cart"] == null)
            {
                IList<Cart> cart = new List<Cart>();
                Session["Cart"] = cart;
            }
            return Session["Cart"] as IList<Cart>;
        }

        public ActionResult ShowOrder()
        {
            IList<Cart> carts = GetCart();
            ViewBag.Product = carts;
            ViewBag.totalMoney = carts.Sum(c => c.Product.Price * c.Quantity);
            ViewBag.Object = new SelectList(db.Object, "Object_ID", "Object_Name");
            ViewBag.Occasion = new SelectList(db.Occasion, "Occasion_ID", "Occasion_Name");
            return View();
        }
        [HttpPost]
        public ActionResult ConfirmOrder(ConfirmOrderModel com)
        {
            var code = new { Success = false, Code = -1, Url = "" };
            try
            {
                Users user = Session["Signed"] as Users;

                LocationController lc = new LocationController();
                com.customerProvince = lc.GetProvinceName(com.customerProvince);
                com.customerDistrict = lc.GetDistrictName(com.customerDistrict);
                com.customerWard = lc.GetWardName(com.customerWard);

                //Xác thực thông tin người mua
                int customerID = db.Customer.Where(c => c.Phone == com.customerPhone && !String.IsNullOrEmpty(com.customerPhone)).Select(c => c.Customer_ID).FirstOrDefault();

                string shipping_address = com.customerAddressConsume + ", " + com.customerWard + ", " + com.customerDistrict + ", " + com.customerProvince;

                Order order = new Order();
                order.Order_ID = DateTime.Now.Ticks.ToString();
                order.Order_Date = DateTime.Now;
                order.Status = "Chờ xác nhận";
                order.Total_Amount = com.totalMoney;
                if (customerID == 0)
                {
                    Customer customer = new Customer();
                    customer.Name = com.customerName;
                    customer.Phone = com.customerPhone;
                    customer.Email = com.customerEmail;
                    customer.Address = com.customerAddress;
                    db.Customer.Add(customer);
                    db.SaveChanges();
                    customerID = customer.Customer_ID;
                }
                order.Customer_ID = customerID;
                order.Shipping_Address = shipping_address;
                order.Payment_Method = com.paymentMethod;
                order.ThoiGianNhanHang = com.RecceiveTime;
                db.Order.Add(order);
                db.SaveChanges();


                //Thêm vào bảng Order_Details
                var orderID = order.Order_ID;
                Discount discount = db.Discount.Where(d => d.Discount_Code == com.couponCode).FirstOrDefault();
                IList<Cart> cart = new List<Cart>();
                if (user != null)
                {
                    cart = db.Cart.Where(c => c.User_ID == user.User_ID).ToList();
                    foreach (var item in cart)
                    {
                        Order_Details orderDetails = new Order_Details();
                        orderDetails.Order_ID = orderID;
                        orderDetails.Product_ID = item.Product_ID;
                        orderDetails.Quantity = item.Quantity;
                        orderDetails.Price = (item.Product.Price * item.Quantity) * discount.Discount_Percentage;

                        //Update số lượng sản phẩm trên bảng sản phẩm
                        Product product = db.Product.Where(p => p.Product_ID == item.Product_ID).FirstOrDefault();
                        product.Quantity -= item.Quantity;

                        db.Order_Details.Add(orderDetails);
                        db.SaveChanges();
                        db.Cart.Remove(item);
                        db.SaveChanges();
                    }
                }
                else
                {
                    cart = GetCart();
                    foreach (var item in cart)
                    {
                        Order_Details orderDetails = new Order_Details();
                        orderDetails.Order_ID = orderID;
                        orderDetails.Product_ID = item.Product_ID;
                        orderDetails.Quantity = item.Quantity;
                        if (discount != null)
                        {
                            orderDetails.Price = (item.Product.Price * item.Quantity) * discount.Discount_Percentage;
                        }
                        else
                        {
                            orderDetails.Price = item.Product.Price * item.Quantity;
                        }

                        //Update số lượng sản phẩm trên bảng sản phẩm
                        Product product = db.Product.Where(p => p.Product_ID == item.Product_ID).FirstOrDefault();
                        product.Quantity -= item.Quantity;

                        db.Order_Details.Add(orderDetails);
                        db.SaveChanges();
                    }
                }


                //Thêm vào bảng Shipping
                Shipping shipping = new Shipping()
                {
                    Shipping_Method = com.ShippingMethod,
                    Order_ID = orderID,
                    Shipping_Status = "Đang giao hàng",
                    SDTNguoiNhan = com.customerPhoneConsume,
                    TenNguoiNhan = com.customerNameConsume
                };
                db.Shipping.Add(shipping);
                db.SaveChanges();

                if (com.paymentMethod.Equals("Tiền mặt/COD"))
                {
                    code = new { Success = true, Code = 1, Url = "/Order/OrderSuccess" };
                }
                else
                {
                    var url = UrlPayment(0, order.Order_ID); //Thanh toán bằng chuyển khoản ngân hàng
                    code = new { Success = true, Code = 2, Url = url };
                }
                return Json(code, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(code, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult VNPayReturn()
        {
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                String TerminalID = Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.QueryString["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    Order order = db.Order.Where(o => o.Order_ID == orderId.ToString()).FirstOrDefault();
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        //Thanh toan thanh cong
                        ViewBag.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                        order.Status = "Đã thanh toán";
                        //log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                        //log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
                        order.Status = "Lỗi thanh toán";
                    }
                    order.Payment_Method = "Thanh toán online";
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }
            return View();
        }

        public ActionResult OrderSuccess()
        {
            return View();
        }
        public ActionResult OrderFail()
        {
            return View();
        }
        public string UrlPayment(int TypePaymentVN, string orderCode)
        {
            var urlPayment = "";
            var order = db.Order.Where(o => o.Order_ID == orderCode).FirstOrDefault();
            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            var Total_Amount = (long)order.Total_Amount*100;

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", Total_Amount.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000

            if (TypePaymentVN == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVN == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVN == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            var orderDate = order.Order_Date.ToString();
            vnpay.AddRequestData("vnp_CreateDate", order.Order_Date.Value.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng: " + order.Order_ID);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.Order_ID.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            return urlPayment;
        }
        public ActionResult TraCuuDonHang()
        {
            return View();
        }
        public ActionResult TraCuuDonHang(string SDT)
        {
            return View();
        }
    }
}