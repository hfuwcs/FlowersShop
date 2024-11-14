using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;
namespace FlowersShop.Controllers
{
    public class OrderController : Controller
    {
        QL_BanHoaEntities db = new QL_BanHoaEntities();
        /*
           SELECT TOP (1000) [Order_ID]
                          ,[Order_Date]
                          ,[Status]
                          ,[Total_Amount]
                          ,[Customer_ID]
                          ,[Shipping_Address]
                          ,[Payment_Method]
                          ,[User_ID]
          FROM [QL_BanHoa].[dbo].[Order]

        SELECT TOP (1000) [Order_Detail_ID]
                      ,[Order_ID]
                      ,[Product_ID]
                      ,[Quantity]
                      ,[Price]
        FROM [QL_BanHoa].[dbo].[Order_Details]


         */

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
        public ActionResult ConfirmOrder()
        {
            Order order = new Order();
            order.Order_Date = DateTime.Now;
            order.Status = "Chờ xác nhận";
            order.Total_Amount = 0;
            order.Customer_ID = 1;
            order.Shipping_Address = "Hà Nội";
            order.Payment_Method = "Thanh toán khi nhận hàng";
            order.User_ID = 1;
            db.Order.Add(order);
            db.SaveChanges();
            int orderID = order.Order_ID;
            List<Cart> cart = db.Cart.Where(c => c.User_ID == 1).ToList();
            foreach (var item in cart)
            {
                Order_Details orderDetails = new Order_Details();
                orderDetails.Order_ID = orderID;
                orderDetails.Product_ID = item.Product_ID;
                orderDetails.Quantity = item.Quantity;
                orderDetails.Price = item.Product.Price*item.Quantity;
                db.Order_Details.Add(orderDetails);
                db.SaveChanges();
                db.Cart.Remove(item);
                db.SaveChanges();
            }
            return RedirectToAction("ShowOrder");
        }
    }
}