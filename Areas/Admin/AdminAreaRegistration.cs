﻿using System.Web.Mvc;

namespace FlowersShop.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "DangNhap", id = UrlParameter.Optional },
                namespaces: new[] { "FlowersShop.Areas.Admin.Controllers" }
            );
        }
    }
}