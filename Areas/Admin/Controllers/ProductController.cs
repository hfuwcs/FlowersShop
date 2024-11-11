﻿using System;
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
            
            ViewBag.Color = new SelectList(db.Color, "Color_ID", "Color_Name");
            return View();
        }

        [HttpPost]
        public ActionResult DeleteProduct(string productid)
        {
            int id = int.Parse(productid);
            Product product = db.Product
                                .Include(p => p.Color)
                                .Include(p => p.Object)
                                .Include(p => p.Occasion)
                                .Include(p => p.Presentation)
                                .SingleOrDefault(p => p.Product_ID == id);
            DeleteIntermediateTable(product);
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
            Product product = db.Product
                                .Include(p => p.Color)
                                .Include(p => p.Object)
                                .Include(p => p.Occasion)
                                .Include(p => p.Presentation)
                                .SingleOrDefault(p => p.Product_ID == id);

            product.SelectedColorIds = product.Color.Select(c => c.Color_ID).ToList();
            product.SelectedObjectIds = product.Object.Select(o => o.Object_ID).ToList();
            product.SelectedOccasionIds = product.Occasion.Select(o => o.Occasion_ID).ToList();
            product.SelectedPresentationIds = product.Presentation.Select(p => p.Presentation_ID).ToList();
            ViewBag.Color = new MultiSelectList(db.Color, "Color_ID", "Color_Name", product.SelectedColorIds);
            ViewBag.Object = new MultiSelectList(db.Object, "Object_ID", "Object_Name", product.SelectedObjectIds);
            ViewBag.Occasion = new MultiSelectList(db.Occasion, "Occasion_ID", "Occasion_Name", product.SelectedOccasionIds);

            ViewBag.Presentation = new MultiSelectList(db.Presentation, "Presentation_ID", "Presentation_Name", product.SelectedPresentationIds);
            return PartialView("_EditModal",product);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product, FormCollection f)
        {   
            var multiColorID = f["SelectedColorIds"]?.Split(',');
            var multiObjectID = f["SelectedObjectIds"]?.Split(',');
            var multiOccasionID = f["SelectedOccasionIds"]?.Split(',');
            var multiPresentationID = f["SelectedPresentationIds"]?.Split(',');

            var selectedColors = db.Color.Where(c => multiColorID.Contains(c.Color_ID.ToString())).ToList();
            var selectedObjects = db.Object.Where(o => multiObjectID.Contains(o.Object_ID.ToString())).ToList();
            var selectedOccasions = db.Occasion.Where(o => multiOccasionID.Contains(o.Occasion_ID.ToString())).ToList();
            var selectedPresentations = db.Presentation.Where(p => multiPresentationID.Contains(p.Presentation_ID.ToString())).ToList();

            var producttemp = db.Product
                                .Include(p => p.Color)
                                .Include(p => p.Object)
                                .Include(p => p.Occasion)
                                .Include(p => p.Presentation)
                                .SingleOrDefault(p => p.Product_ID == product.Product_ID);
            DeleteIntermediateTable(producttemp);
            AddIntermediateTable(producttemp, selectedColors, selectedObjects, selectedOccasions, selectedPresentations);

            int rowAffected = db.SaveChanges();

            if (rowAffected > 0)
            {
                return Json(new { success = true, product = product });
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public ActionResult TestingProduct()
        {
            return Json(new { success = true, color = db.Color.FirstOrDefault() },JsonRequestBehavior.AllowGet);
        }
        public ActionResult DetailProduct(int id)
        {
            Product product = db.Product.Find(id);
            return View(product); 
        }
        public void DeleteIntermediateTable(Product product)
        {
            foreach (var color in product.Color.ToList())
            {
                if (color != null)
                {
                    color.Product.Remove(product);
                }
            }
            foreach (var objectt in product.Object.ToList())
            {
                if (objectt != null)
                {
                    objectt.Product.Remove(product);
                }
            }
            foreach (var occasion in product.Occasion.ToList())
            {
                if (occasion != null)
                {
                    occasion.Product.Remove(product);
                }
            }
            foreach (var presentation in product.Presentation.ToList())
            {
                if (presentation != null)
                {
                    presentation.Product.Remove(product);
                }
            }
        }

        public void AddIntermediateTable(Product product, List<Color> selectedColors, List<Repository.Object> selectedObjects, List<Occasion> selectedOccasions, List<Presentation> selectedPresentations)
        {
            foreach (var color in selectedColors)
            {
                if (color != null)
                    color.Product.Add(product);
            }
            foreach (var objectt in selectedObjects)
            {
                if (objectt != null)
                    objectt.Product.Add(product);
            }
            foreach (var occasion in selectedOccasions)
            {
                if (occasion != null)
                    occasion.Product.Add(product);
            }
            foreach (var presentation in selectedPresentations)
            {
                if (presentation != null)
                    presentation.Product.Add(product);
            }
        }
    }
}