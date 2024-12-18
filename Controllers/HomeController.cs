﻿using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowersShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            QL_BanHoaEntities db = new QL_BanHoaEntities();
            var order = db.Order.FirstOrDefault();
            DateTime orderstr = order.Order_Date.Value;
            string str = orderstr.ToString("yyyyMMddHHmmss");
            ViewBag.username = TempData["user"];
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}