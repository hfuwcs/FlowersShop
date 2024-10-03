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
        UserProc obj = new UserProc();
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
        public ActionResult DangNhap(Users user)
        {
                if (obj.XacThucUser(user))
                {
                    //Sử dụng TempData để chuyển sang Action của Controller khác
                    Session["Signed"] = user.UserName;//Tạo session để biết người dùng đã log in chưa
                    TempData["user"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                ViewBag.error = "Sai tên đăng nhập hoặc mật khẩu";
                    //ModelState.AddModelError("UserName", "Sai tên đăng nhập hoặc mật khẩu");
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
            user.Role_id = 2;
            if (obj.CheckUserName(user.UserName))
            {
                ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại");//Thêm "Error" vào Model để
                //kiểm xuất ra màn hình người dùng
            }
            if (ModelState.IsValid)//Nếu dòng if ở trên đúng là ModelState.AddModelError được thực thi
                // thì dòng if này sẽ sai
            {
                db.Users.Add(user);
                db.SaveChanges();
                TempData["user"] = user;
                return RedirectToAction("ThanhCong", "User");
            }
            else//Dòng if trên sai thì dòng dưới này sẽ thực hiện
            {
                return View();
                //return RedirectToAction("Index", "Home");
            }
        }
    }
}