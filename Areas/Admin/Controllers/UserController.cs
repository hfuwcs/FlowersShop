using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;
using FlowersShop.Repository;//Các context, class được lưu ở Repository


namespace FlowersShop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        UserBusiness obj = new UserBusiness();
        QL_BanHoaEntities db = new QL_BanHoaEntities();
        public ActionResult DangNhap()
        {
            if (Session["Admin"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DangNhap(Users user)
        {
                 if (obj.IsAdmin(user))
                {
                    Session["Admin"] = user.UserName;
                    return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
                }
                else
                {
                    ViewBag.error = "Sai tên đăng nhập hoặc mật khẩu";
                    return View();
                }
        }
        public ActionResult ThanhCong()
        {
            ViewBag.username = TempData["user"];
            return View();
        }
        public ActionResult DangKy()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DangKy(Users user)
        {
            user.Role_ID = 3;
            if (obj.CheckUserName(user.UserName))
            {
                ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại");
            }
            if (ModelState.IsValid)
            {
                user.Password = Cipher.EncryptSHA256(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("ThanhCong", "User");
            }
            else
            {
                return View();
            }
        }

        public ActionResult DangXuat()
        {
            Session["Admin"] = null;
            return RedirectToAction("Index", "Dashboard");
        }
    }
}