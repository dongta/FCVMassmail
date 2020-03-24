using FCV_DutchLadyMail_Model.Common;
using FCV_DutchLadyMail_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FCV_DutchLadyMail_Model.Attribute
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // format permission : controller-action
            string requiredPermission = String.Format("{0}-{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);
            var Identity = (IdentityUser)filterContext.HttpContext.Session["Identity"];
            MassMailsDbContext database = new MassMailsDbContext();
            if (Identity != null)
            {
                LOG_ACCESS log = new LOG_ACCESS();
                log.page = requiredPermission;
                log.created_at = System.DateTime.Now;
                log.User_Id = Identity.UserId;
                AuthUser requestingUser = new AuthUser(Identity.UserId);
                if (!requestingUser.HasPermission(requiredPermission) & !requestingUser.IsSysAdmin)
                {

                    log.status = false;
                    database.LOG_ACCESS.Add(log);
                    database.SaveChanges();

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Unauthorised" } });
                }
                else
                {
                    log.status = true;
                    database.LOG_ACCESS.Add(log);
                    database.SaveChanges();
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Login" }, { "returnUrl", filterContext.HttpContext.Request.RawUrl } });
            }

        }
    }
}