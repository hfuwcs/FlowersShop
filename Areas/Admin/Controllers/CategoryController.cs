using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowersShop.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        static QL_BanHoaEntities db = new QL_BanHoaEntities();
        public ActionResult ShowAllCategory()
        {
            ViewBag.Color = db.Color.ToList();
            ViewBag.Object = db.Object.ToList();
            ViewBag.Presentation = db.Presentation.ToList();
            ViewBag.Occasion = db.Occasion.ToList();
            return View();
        }
        public ActionResult ColorDetail(int id)
        {
            Color color = db.Color.Find(id);
            return View(color);
        }
        public ActionResult ObjectDetail(int id)
        {
            Repository.Object objectt = db.Object.Find(id);
            return View(objectt);
        }
        public ActionResult PresentationDetail(int id)
        {
            Presentation presentation = db.Presentation.Find(id);
            return View(presentation);
        }
        public ActionResult OccasionDetail(int id)
        {
            Occasion occasion = db.Occasion.Find(id);
            return View(occasion);
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(string categoryNameInput, int category_id)
        {
            try
            {
                if (category_id == 1)
                {
                    db.Color.AddOrUpdate(new Color { Color_Name = categoryNameInput });
                }
                else if (category_id == 2)
                {
                    db.Object.AddOrUpdate(new Repository.Object { Object_Name = categoryNameInput });
                }
                else if (category_id == 3)
                {
                    db.Presentation.AddOrUpdate(new Presentation { Presentation_Name = categoryNameInput });
                }
                else if (category_id == 4)
                {
                    db.Occasion.AddOrUpdate(new Occasion { Occasion_Name = categoryNameInput });
                }
                int rowAffected = db.SaveChanges();
                if (rowAffected <= 0) { 
                    return Json(new { success = false, message = "Thêm thất bại" }); 
                }

                return Json(new
                {
                    success = true
                }
                );
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        [HttpPost]
        public ActionResult EditCategory(int categoryType, int category_id, string categoryNameInput)
        {
            try
            {
                if (categoryType == 1)
                {
                    Color color = db.Color.Find(category_id);
                    color.Color_Name = categoryNameInput;
                    db.Color.AddOrUpdate(color);
                }
                else if (categoryType == 2)
                {
                    Repository.Object objectt = db.Object.Find(category_id);
                    objectt.Object_Name = categoryNameInput;
                    db.Object.AddOrUpdate(objectt);
                }
                else if (categoryType == 3)
                {
                    Presentation presentation = db.Presentation.Find(category_id);
                    presentation.Presentation_Name = categoryNameInput;
                    db.Presentation.AddOrUpdate(presentation);
                }
                else if (categoryType == 4)
                {
                    Occasion occasion = db.Occasion.Find(category_id);
                    occasion.Occasion_Name = categoryNameInput;
                    db.Occasion.AddOrUpdate(occasion);
                }
                int rowAffected = db.SaveChanges();
                if (rowAffected <= 0)
                {
                    ViewBag.Message = "Cập nhật thất bại";
                    return View();
                }

                return RedirectToAction("ShowAllCategory");
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}