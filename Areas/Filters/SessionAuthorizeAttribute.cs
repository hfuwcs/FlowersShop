using System;
using System.Web;
using System.Web.Mvc;

namespace FlowersShop.Filters
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Lấy URL hiện tại
            var url = filterContext.HttpContext.Request.Url?.AbsolutePath;

            //KT xem thuộc Area Admin không
            if (url != null && !url.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase))
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            // Bỏ qua trang đăng nhập
            if (url != null && url.StartsWith("/Admin/User/DangNhap", System.StringComparison.OrdinalIgnoreCase))
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            if (HttpContext.Current.Session["Admin"] == null)
            {
                // Chuyển hướng đến trang đăng nhập
                filterContext.Result = new RedirectResult("~/Admin/User/DangNhap");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
