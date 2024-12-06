using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowersShop.Areas.Admin.Controllers
{
    public class DiscountController : Controller
    {
        // GET: Admin/Discount
        static QL_BanHoaEntities db = new QL_BanHoaEntities();
        public ActionResult ShowAllDiscountCode()
        {
            IList<Discount> discount = db.Discount.ToList();
            return View(discount);
        }
        [HttpGet]
        public ActionResult EditDiscountCode(int id)
        {
            Discount discount = db.Discount.FirstOrDefault(x => x.Discount_ID == id);
            return PartialView("_EditDiscountCode",discount);
        }
        [HttpPost]
        public ActionResult EditDiscountCode(Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Discount.AddOrUpdate(discount);
                db.SaveChanges();
                return RedirectToAction("ShowAllDiscountCode");
            }
            return View(discount);
        }
        public ActionResult AddDiscountCode()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDiscountCode(Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Discount.Add(discount);
                db.SaveChanges();
                return RedirectToAction("ShowAllDiscountCode");
            }
            return View(discount);
        }
        [HttpGet]
        public ActionResult DeleteDiscountCode()
        {
            return PartialView("_DeleteDiscountCode");
        }

        [HttpPost]
        public ActionResult DeleteDiscountCode(int id)
        {
            Discount discount = db.Discount.FirstOrDefault(x => x.Discount_ID == id);
            db.Discount.Remove(discount);
            int rowAffected = db.SaveChanges();
            if(rowAffected > 0)
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}