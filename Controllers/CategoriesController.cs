using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace FlowersShop.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult ShowCate()
        {
            IEnumerable<Category> categories=null;

            using (var client = new HttpClient())
            {
                //Consume WebAPI GET method (Sử dụng method GET từ Categories API)
                client.BaseAddress = new Uri("https://localhost:44353/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Categories");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Category>>();
                    readTask.Wait();

                    categories = readTask.Result;
                }

                else//Web api send error (Không đúng thì sẽ trả lỗi)
                {
                    categories = Enumerable.Empty<Category>();
                    ModelState.AddModelError(string.Empty, "Sevver lỗi, hãy liên hệ quản trị viên");
                }
                return View(categories);
            }
        }

        public ActionResult CreateCategory()
        {
            return View(); 
        }

    }
}