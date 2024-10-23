using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowersShop.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        QL_BanHoaEntities db = new QL_BanHoaEntities();
        public ActionResult AddTest()
        {
            ViewBag.ID = new SelectList(db.Test, "ID", "NAME");
            return View();
        }
    }
}