using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCVStockTool.Filters
{
    public class AccountantRoleAttribute : SecurityAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (RoleName != "Accountants" && RoleName != "Administrators")
            {
               filterContext.Result = new RedirectResult("/Account/Login");
                //filterContext.HttpContext.Response.Redirect("/Account/Login");
            }
            else
            {
                return;
            }
            
        }
    }
}