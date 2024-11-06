using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowersShop.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        [HttpGet]
        public ActionResult InputTest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InputTest(string email)
        {
            if (email != null)
            {
                Session["email"] = email;
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.email = Session["email"] as string;
            return View();
        }
    }
}