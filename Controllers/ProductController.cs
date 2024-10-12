using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;
using FlowersShop.Repository;
using System.Data.Entity;

namespace FlowersShop.Controllers
{
    public class ProductController : Controller
    {
        ProductBusiness objproduct = new ProductBusiness();
        CategoryBusiness objCategory = new CategoryBusiness();
        QL_BanHoaEntities db = new QL_BanHoaEntities();
        [HttpGet]
        public ActionResult ShowProduct()
        {
            IEnumerable<Product> products = new List<Product>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/");

                var responseTask = client.GetAsync("Products");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask=result.Content.ReadAsAsync<IList<Product>>();
                    readTask.Wait();

                    products = readTask.Result;
                }
                else//Web api send error (Không đúng thì sẽ trả lỗi)
                {
                    products = Enumerable.Empty<Product>();
                    ModelState.AddModelError(string.Empty, "Sevver lỗi, hãy liên hệ quản trị viên");
                }
                return View(products);
            }
        }
        public ActionResult SearchProduct(String txt_Search)
        {
            IEnumerable<Product> products = new List<Product>();
            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri("https://localhost:44353/api/");

                var responseTask = client.GetAsync("Products/?ProductName=" + txt_Search);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode) { 
                    var readTask = result.Content.ReadAsAsync<IList<Product>>();
                    products = readTask.Result;
                }
                else//Web api send error (Không đúng thì sẽ trả lỗi)
                {
                    products = Enumerable.Empty<Product>();
                    ModelState.AddModelError(string.Empty, "Sevver lỗi, hãy liên hệ quản trị viên");
                }
                return View(products);
            }
        }
        [HttpGet]
        public ActionResult CreateProduct() {
            ViewBag.Category_ID = new SelectList(db.Category, "Category_ID", "Category_Name");
            return View(); 
        }
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            //List<Category> SelectListItem = objCategory.GetCategory();    
            return View();
        }
    }
}