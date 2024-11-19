using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;
using FlowersShop.ApiControllers;
using Newtonsoft.Json;

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
            ViewBag.Product = db.Cart.Where(c => c.User_ID == 1).ToList();
            ViewBag.Object = new SelectList(db.Object, "Object_ID", "Object_Name");
            ViewBag.Occasion = new SelectList(db.Occasion, "Occasion_ID", "Occasion_Name");
            return View();
        }
        [HttpPost]
        public ActionResult ConfirmOrder(
            //Người đặt
            string customerName,
            string customerPhone,
            string customerEmail,
            string customerAddress,
            string paymentMethod,
            //Người nhận
            string customerNameConsume,
            string customerPhoneConsume,
            string customerAddressConsume,
            string customerProvince,
            string customerDistrict,
            string customerWard,
            string RecceiveTime,

            //Mã giảm giá
            string couponCode,
            //Phương thức giao hàng
            string ShippingMethod
            )
        {

            Users user = Session["Signed"] as Users;

            LocationController lc = new LocationController();
            customerProvince = lc.GetProvinceName(customerProvince);
            customerDistrict = lc.GetDistrictName(customerDistrict);
            customerWard = lc.GetWardName(customerWard);

            //Xác thực thông tin người mua
            int customerID = db.Customer.Where(c => c.Phone == customerPhone && !String.IsNullOrEmpty(customerPhone)).Select(c => c.Customer_ID).FirstOrDefault();

            string shipping_address = customerAddressConsume + ", " + customerWard + ", " + customerDistrict + ", " + customerProvince;

            Order order = new Order();
            order.Order_Date = DateTime.Now;
            order.Status = "Chờ xác nhận";
            order.Total_Amount = 0;
            if (customerID == 0)
            {
                Customer customer = new Customer();
                customer.Name = customerName;
                customer.Phone = customerPhone;
                customer.Email = customerEmail;
                customer.Address = customerAddress;
                db.Customer.Add(customer);
                db.SaveChanges();
                customerID = customer.Customer_ID;
            }
            order.Shipping_Address = shipping_address;
            order.Payment_Method = paymentMethod;
            order.ThoiGianNhanHang = RecceiveTime;
            db.Order.Add(order);
            db.SaveChanges();


            //Thêm vào bảng Order_Details
            int orderID = order.Order_ID;
            Discount discount = db.Discount.Where(d => d.Discount_Code == couponCode).FirstOrDefault();
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
                    orderDetails.Price = (item.Product.Price * item.Quantity) * discount.Discount_Percentage;
                    db.Order_Details.Add(orderDetails);
                    db.SaveChanges();
                }
            }

            //Thêm vào bảng Shipping
            Shipping shipping = new Shipping()
            {
                Shipping_Method = ShippingMethod,
                Order_ID = orderID,
                Shipping_Status = "Đang giao hàng",
                SDTNguoiNhan = customerPhoneConsume,
                TenNguoiNhan = customerNameConsume
            };
            db.Shipping.Add(shipping);
            db.SaveChanges();

            return RedirectToAction("ShowProduct","Product");
        }
        public ActionResult OrderSuccessful()
        {
            return View();
        }
    }
}