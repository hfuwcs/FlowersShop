using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;
using FlowersShop.Repository;//Các context, class được lưu ở Repository


namespace FlowersShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        UserBusiness obj = new UserBusiness();
        QL_BanHoaEntities db = new QL_BanHoaEntities();
        public ActionResult DangNhap()
        {
            if (Session["Signed"] != null)//Signed = Đã đăng nhập
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(Users user)
        {
                if (obj.IsUser(user))
                {
                    Session["Signed"] = user.UserName;//Tạo session để biết người dùng đã log in chưa
                    TempData["user"] = user;
                    return RedirectToAction("Index", "Home");
                }
                //else if (obj.IsAdmin(user))
                //{
                //Session["Admin"] = user.UserName;
                //return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
                //}
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
                ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại");//Thêm "Error" vào Model để
                //kiểm xuất ra màn hình người dùng
            }
            if (ModelState.IsValid)
            {
                user.Password = Cipher.EncryptSHA256(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
                TempData["user"] = user;
                return RedirectToAction("ThanhCong", "User");
            }
            else
            {
                return View();
                //return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DangXuat()
        {
            Session["Signed"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}