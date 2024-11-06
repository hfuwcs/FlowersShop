using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;
using FlowersShop.Repository;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Data.Entity.Migrations;


namespace FlowersShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        QL_BanHoaEntities db = new QL_BanHoaEntities();
        ProductBusiness bn = new ProductBusiness();
        [HttpGet]
        public ActionResult ShowProduct()
        {
            ViewBag.Color = new SelectList(db.Color, "Color_ID", "Color_Name");
            ViewBag.Object = new SelectList(db.Object, "Object_ID", "Object_Name");
            ViewBag.Occasion = new SelectList(db.Occasion, "Occasion_ID", "Occasion_Name");
            ViewBag.Presentation = new SelectList(db.Presentation, "Presentation_ID", "Presentation_Name");
            IList<Product> products = bn.GetData();
            return View(products);
        }

        [HttpGet]
        public ActionResult SearchProduct(String ProductName)
        {
            IList<Product> products = bn.SearchProduct(ProductName);
            return View(products);           
        }
        [HttpGet]
        public ActionResult CreateProduct() {
            //Truyền Categories xuống View thôi ;)
            //Fix this one dude
            ViewBag.Color = new SelectList(db.Color, "Color_ID", "Color_Name");

            if (ViewBag.Color == null) {
                return HttpNotFound();
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (!bn.AddProduct(product))
            {
                ModelState.AddModelError(string.Empty, "Thiếu thông tin sản phẩm");
            }
            //Fix this One
            ViewBag.Color = new SelectList(db.Color, "Color_ID", "Color_Name");
            return View();
        }

        [HttpPost]
        public ActionResult DeleteProduct(string productid)
        {
            int id = int.Parse(productid);
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            int rowAffected = db.SaveChanges();
            if (rowAffected > 0)
            {
                return Json(true);
            }
            return Json(false); 
        }
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            Product product = db.Product.Find(id);
            ViewBag.Color = new SelectList(db.Color, "Color_ID", "Color_Name");
            ViewBag.Object = new SelectList(db.Object, "Object_ID", "Object_Name");
            ViewBag.Occasion = new SelectList(db.Occasion, "Occasion_ID", "Occasion_Name");
            ViewBag.Presentation = new SelectList(db.Presentation, "Presentation_ID", "Presentation_Name");
            return PartialView("_EditModal",product);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (product == null) { return HttpNotFound(); }
            ViewBag.Color = new SelectList(db.Color, "Color_ID", "Color_Name");
            ViewBag.Object = new SelectList(db.Object, "Object_ID", "Object_Name");
            ViewBag.Occasion = new SelectList(db.Occasion, "Occasion_ID", "Occasion_Name");
            ViewBag.Presentation = new SelectList(db.Presentation, "Presentation_ID", "Presentation_Name");
            db.Product.AddOrUpdate(product);
            db.SaveChanges();
            return RedirectToAction("ShowProduct");
        }


        //[HttpGet]
        //public ActionResult EditProduct(int id)
        //{
        //    Product product = db.Product.Find(id);
        //    ViewBag.Category_ID = new SelectList(db.Color, "Category_ID", "Category_Name");
        //    return View(product);
        //}
        //[HttpPost]
        //public ActionResult EditProduct(Product product)
        //{
        //    if(product == null) { return HttpNotFound();}
        //    ViewBag.Category_ID = new SelectList(db.Color, "Category_ID", "Category_Name");
        //    db.Product.AddOrUpdate(product);
        //    db.SaveChanges();
        //    return RedirectToAction("ShowProduct");
        //}
    }
}