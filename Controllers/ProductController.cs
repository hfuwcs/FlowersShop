using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;
using FlowersShop.Repository;

namespace FlowersShop.Controllers
{
    public class ProductController : Controller
    {
        ConnectProduct obj = new ConnectProduct();
        public ActionResult ShowProduct()
        {
            List<Product> products = obj.GetData();
            return View(products); 
        }
        [HttpPost]
        public ActionResult SearchProduct(String txt_Search)
        {
            List<Product> products = obj.SearchProduct(txt_Search);
            return View(products);
        }
    }
}