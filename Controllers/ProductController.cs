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
        public ActionResult SearchProduct()
        {
            ViewBag.Colors = db.Color.ToList();
            ViewBag.Occasions = db.Occasion.ToList();
            ViewBag.Objects = db.Object.ToList();
            ViewBag.Presentations = db.Presentation.ToList();
            return View(); 
        }

        [HttpPost]
        public ActionResult SearchProduct(string ProductName)
        {
            IList<Product> products = bn.SearchProduct(ProductName);
            IList<ProductDTO> productDTOList = new List<ProductDTO>();
            foreach (Product product in products) {
                ProductDTO productDTO = new ProductDTO();
                productDTO.Product_ID = product.Product_ID;
                productDTO.Name = product.Name;
                productDTO.Price = product.Price;
                productDTO.Image = product.Image;
                productDTO.Description = product.Description;
                productDTO.Quantity = product.Quantity;
                productDTOList.Add(productDTO);
            }
            return Json(productDTOList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchProductByCategory(int[] SelectedColorIds = null, int[] SelectedOccasionIds = null, int[] SelectedObjectIds = null, int[] SelectedPresentationIds = null)
        {
            // Truy xuất tất cả sản phẩm trước khi lọc
            var products = db.Product.AsQueryable();

            // Lọc theo màu sắc (sản phẩm cần chứa bất kỳ màu nào được chọn)
            if (SelectedColorIds != null && SelectedColorIds.Any())
            {
                products = products.Where(p => p.Color.Any(c => SelectedColorIds.Contains(c.Color_ID)));
            }

            // Lọc theo dịp sử dụng (sản phẩm cần chứa bất kỳ dịp nào được chọn)
            if (SelectedOccasionIds != null && SelectedOccasionIds.Any())
            {
                products = products.Where(p => p.Occasion.Any(o => SelectedOccasionIds.Contains(o.Occasion_ID)));
            }

            // Lọc theo đối tượng (sản phẩm cần chứa bất kỳ đối tượng nào được chọn)
            if (SelectedObjectIds != null && SelectedObjectIds.Any())
            {
                products = products.Where(p => p.Object.Any(o => SelectedObjectIds.Contains(o.Object_ID)));
            }

            // Lọc theo cách trình bày (sản phẩm cần chứa bất kỳ kiểu trình bày nào được chọn)
            if (SelectedPresentationIds != null && SelectedPresentationIds.Any())
            {
                products = products.Where(p => p.Presentation.Any(pr => SelectedPresentationIds.Contains(pr.Presentation_ID)));
            }

            // Trả về kết quả dưới dạng PartialView cho AJAX
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ProductListPartial", products.ToList());
            }

            return View(products.ToList());
        }

    }
}