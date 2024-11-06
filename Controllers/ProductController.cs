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


namespace FlowersShop.Controllers
{
    public class ProductController : Controller
    {
        QL_BanHoaEntities db = new QL_BanHoaEntities();
        ProductBusiness bn = new ProductBusiness();
        [HttpGet]
        public ActionResult ShowProduct()
        {
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
            ViewBag.Category_ID = new SelectList(db.Color, "Category_ID", "Category_Name");
            if (ViewBag.Category_ID == null) {
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
            ViewBag.Category_ID = new SelectList(db.Color, "Category_ID", "Category_Name");
            return View();
        }

        [HttpGet]
        public ActionResult DeleteProduct() {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteProduct(int id) {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            Product product = db.Product.Find(id);
            ViewBag.Category_ID = new SelectList(db.Color, "Category_ID", "Category_Name");
            return View(product);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if(product == null) { return HttpNotFound();}
            ViewBag.Category_ID = new SelectList(db.Color, "Category_ID", "Category_Name");
            db.Product.AddOrUpdate(product);
            db.SaveChanges();
            return RedirectToAction("ShowProduct");
        }
    }
}