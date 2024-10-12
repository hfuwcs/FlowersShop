using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace FlowersShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
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
                else//Web api send error (Không đúng thì sẽ trả lỗi)
                {
                    ModelState.AddModelError(string.Empty, "Sevver lỗi, hãy liên hệ quản trị viên");
                }
                return View(carts);
            }
        }
    }
}