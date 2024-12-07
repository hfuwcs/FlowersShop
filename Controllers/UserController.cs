using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
        public ActionResult AccountDetail(int User_ID)
        {
            Users user = db.Users.Find(User_ID);
            return View(user);
        }
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
                    user = db.Users.FirstOrDefault(u => u.UserName.Equals(user.UserName));
                    Session["Signed"] = user;
                    if (Session["Cart"] != null)
                    {
                        IList<Cart> carts = Session["Cart"] as IList<Cart>;
                        foreach(var item in carts)
                        {
                            var existingProductInCart = db.Cart.SingleOrDefault(c => c.Product_ID == item.Product_ID 
                            && c.User_ID == user.User_ID);
                            if (existingProductInCart != null)
                            {
                                existingProductInCart.Quantity += item.Quantity;
                                db.Cart.AddOrUpdate(existingProductInCart);
                            }
                            else
                            {
                                Cart newItem = item;
                                newItem.User_ID = user.User_ID;
                                db.Cart.AddOrUpdate(newItem);
                            }
                        }
                        db.SaveChanges();
                    }
                    return RedirectToAction("ShowProduct", "Product");
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