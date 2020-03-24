using FCV_DutchLadyMail_Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCV_DutchLadyMail.Controllers
{
    public class BaseController : Controller
    {
        public IdentityUser _Identity;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _Identity = (IdentityUser)Session["Identity"];
            if (_Identity == null)
                filterContext.Result =
                    new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Login", action = "Index" }));
            base.OnActionExecuting(filterContext);
        }
    }
}