using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCVStockTool.Filters
{
    public class SecurityAttribute : ActionFilterAttribute
    {
        public string RoleName { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var ActionName = filterContext.ActionDescriptor.ActionName;
            //var ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //var WebAction = ControllerName + "/" + ActionName;

            /*
             * AUTHENTICATION
             */
            HttpCookie logonCookie = filterContext.HttpContext.Request.Cookies["StockTool"];
            if (logonCookie != null)
            {
                RoleName = logonCookie["Role"].ToString();
            }
            else
            {
                filterContext.Result = new RedirectResult("/Account/Login");
                //filterContext.HttpContext.Response.Redirect("/Account/Login");
            }
        }
    }
}