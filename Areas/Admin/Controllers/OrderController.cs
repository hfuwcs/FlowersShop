using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowersShop.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        QL_BanHoaEntities db = new QL_BanHoaEntities();
        public ActionResult ShowOrder()
        {
            IList<Order> orders = db.Order.OrderByDescending(o=>o.Status.Equals("Chờ xác nhận")).ToList();
            return View(orders);
        }
        [HttpPost]
        public ActionResult ConfirmOrder(string id)
        {
            Order order = db.Order.Find(id);
            order.Status = "Đã xác nhận";
            int rowAffected = db.SaveChanges();
            if (rowAffected > 0)
            {
                return Json(true);
            }
            return Json(false);
        }
        [HttpPost]
        public ActionResult CancelOrder(string id)
        {
            Order order = db.Order.Find(id);
            order.Status = "Đã hủy";
            int rowAffected = db.SaveChanges();
            if (rowAffected > 0)
            {
                return Json(true);
            }
            return Json(false);
        }
        public ActionResult DeleteOrder(string id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
            int rowAffected = db.SaveChanges();
            if (rowAffected > 0)
            {
                return RedirectToAction("ShowOrder");
            }
            return RedirectToAction("ShowOrder");
        }
    }
}