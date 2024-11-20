using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace FlowersShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        QL_BanHoaEntities db = new QL_BanHoaEntities();

        public ActionResult ShowCartByID(int Cid)
        {
            IEnumerable<Cart> carts = new List<Cart>();
            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri("https://localhost:44353/api/");

                var responseTask = client.GetAsync("Carts/?Cid=" + Cid);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Cart>>();

                    carts = readTask.Result;
                }
                else//Web api send error
                {
                    ModelState.AddModelError(string.Empty, "Sevver lỗi, hãy liên hệ quản trị viên");
                }
                return View(carts);
            }
        }

        public IList<Cart> GetCart()
        {
            if (Session["Cart"] == null)
            {
                IList<Cart> cart = new List<Cart>();
                Session["Cart"] = cart;
                return Session["Cart"] as IList<Cart>;
            }

            IList<Cart> carts = Session["Cart"] as IList<Cart>;
            foreach(var item in carts)
            {
                item.Product = db.Product.Find(item.Product_ID);
            }

            return Session["Cart"] as IList<Cart>;
        }
        public ActionResult ShowCart()
        {
            IList<Cart> carts = GetCart();

            Users user = Session["Signed"] as Users;

            if (user != null)
            {
                carts = db.Cart.Where(c => c.User_ID == user.User_ID).ToList();
            }
            ViewBag.ItemCount = carts.Count;
            ViewBag.TotalPrice = carts.Sum(c => c.Quantity * c.Product.Price);


            return View(carts);
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            IList<Cart> carts = GetCart();
            DateTime Now = DateTime.Now;
            Users user =  Session["Signed"] as Users;

            if (user != null)
            {
                Cart cart = new Cart()
                {
                    User_ID = user.User_ID,
                    Product_ID = id,
                    Quantity = quantity,
                    Created_Date = Now
                };
                if (carts.Where(c => c.Product_ID == id).SingleOrDefault() != null)
                {
                    cart = carts.FirstOrDefault(c => c.Product_ID == id);
                    cart.Quantity += quantity;
                }
                else
                {
                    carts.Add(cart);
                    Session["Cart"] = carts;
                }
                db.Cart.AddOrUpdate(cart);
                int rowAffected = db.SaveChanges();
                if (rowAffected > 0)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                Cart cart = new Cart()
                {
                    User_ID = -1,
                    Product_ID = id,
                    Quantity = quantity,
                    Created_Date = Now
                };
                if (carts.Where(c=>c.Product_ID==id).SingleOrDefault()!=null)
                {
                    cart = carts.FirstOrDefault(c => c.Product_ID == id);
                    cart.Quantity += quantity;
                }
                else 
                { 
                    carts.Add(cart);
                }
                Session["Cart"] = carts;
                return Json(true);
            }
        }

        [HttpPost]
        public ActionResult UpdateCart(int id, int quantity)
        {
            

            IList<Cart> carts = GetCart();
            Users user = Session["Signed"] as Users;
            Cart cart = new Cart();
            if (user != null)
            {
                cart = db.Cart.FirstOrDefault(c => c.Product_ID == id);
                cart.Quantity = quantity;
                Session["Cart"] = carts;
                db.Cart.AddOrUpdate(cart);
                int rowAffected = db.SaveChanges();
                if (rowAffected > 0)
                {
                    return Json(new
                    {
                        success = true,
                        data = new
                        {
                            totalPrice = carts.Sum(c => c.Quantity * c.Product.Price), // Tổng tiền của toàn giỏ hàng
                            itemPrice = quantity * carts.FirstOrDefault(c => c.Product_ID == id).Product.Price // Tổng thành tiền của 1 sản phẩm
                        }
                    });
                }
                return Json(false);
            }
            else
            {
                carts.FirstOrDefault(c => c.Product_ID == id).Quantity=quantity;
                Session["Cart"] = carts;
                return Json(new
                {
                    success = true,
                    data = new
                    {
                        totalPrice = carts.Sum(c => c.Quantity * c.Product.Price), // Tổng tiền của toàn giỏ hàng
                        itemPrice = quantity * carts.FirstOrDefault(c => c.Product_ID == id).Product.Price // Tổng thành tiền của 1 sản phẩm
                    }
                });
            }
        }

        public ActionResult RemoveFromCart(int id)
        {
            IList<Cart> carts = GetCart();
            Users user = Session["Signed"] as Users;
            Cart cart = new Cart();

            if (user != null)
            {
                cart = db.Cart.FirstOrDefault(c => c.Product_ID == id);
                db.Cart.Remove(cart);
                db.SaveChanges();
                return Json(new
                {
                    success = true,
                    data = new
                    {
                        totalPrice = carts.Sum(c => c.Quantity * c.Product.Price), // Tổng tiền của toàn giỏ hàng 
                    }
                });
            }
            else
            {
                carts.Remove(carts.FirstOrDefault(c => c.Product_ID == id));
                Session["Cart"] = carts;
                return Json(new
                {
                    success = true,
                    data = new
                    {
                        totalPrice = carts.Sum(c => c.Quantity * c.Product.Price), // Tổng tiền của toàn giỏ hàng
                    }
                });
            }
        }
    }
}