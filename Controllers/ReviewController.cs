using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Repository;
namespace FlowersShop.Controllers
{
    public class ReviewController : Controller
    {

        //Đừng xem ở đây, tôi quá mệt mỏi để viết tiếp các logic review rồi, bye, sủi đây
        // GET: Review
        [HttpPost]
        [HttpGet]
        public JsonResult GetLoginStatus()
        {
            Users user = Session["Signed"] as Users;
            bool isLoggedIn = user != null; // Kiểm tra trạng thái đăng nhập từ Session
            return Json(new { loggedIn = isLoggedIn }, JsonRequestBehavior.AllowGet);
        }

        // Xử lý đánh giá (Review)
        [HttpPost]
        public JsonResult SubmitReview(int rating, string comment)
        {
            Users user = Session["Signed"] as Users;
            if (user == null)
            {
                return Json(new { success = false, message = "Bạn chưa đăng nhập" });
            }

            if (rating < 1 || rating > 5)
            {
                return Json(new { success = false, message = "Điểm đánh giá không hợp lệ" });
            }

            if (string.IsNullOrWhiteSpace(comment))
            {
                return Json(new { success = false, message = "Bình luận không được để trống" });
            }

            // Lưu đánh giá (giả lập lưu vào database)
            // Example: SaveReviewToDatabase(user, rating, comment);

            return Json(new { success = true, message = "Đánh giá của bạn đã được gửi thành công!" });
        }

    }
}