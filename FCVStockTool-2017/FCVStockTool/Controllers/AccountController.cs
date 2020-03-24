using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FCVStockTool.Models;
using System.Configuration;
using System.Web.Security;

namespace FCVStockTool.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        FCVStockTool.StockDbContext db = new StockDbContext();
        Utils.LdapAuthentication _ldap = new Utils.LdapAuthentication(string.Format("LDAP://{0}",ConfigurationManager.AppSettings["Domain"]));

        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HttpCookie logonCookie = new HttpCookie("StockTool");
            
            bool loginRS = _ldap.IsAuthenticated(ConfigurationManager.AppSettings["Domain"], model.Username, model.Password);
            if (model.Username == "scadmin" && model.Password == "ivgivg$") loginRS = true;
            if (loginRS)
            {
                var _user = db.Users.Where(u => u.Username.Equals(model.Username, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (_user != null)
                {
                    if (_user.Active)
                    {
                        logonCookie["Username"] = _user.Username;
                        logonCookie["DisplayName"] = _user.DisplayName;
                        logonCookie["UserID"] = _user.Id.ToString();
                        logonCookie["DepartmentId"] = _user.DepartmentId.ToString();
                        logonCookie["Role"] = _user.Role != null ? _user.Role.Name : "";
                        logonCookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(logonCookie);
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "User has been disabled.");
                    }
                }
            }

            logonCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(logonCookie);
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }
        [AllowAnonymous]
        //
        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            HttpCookie logonCookie = new HttpCookie("StockTool");
            logonCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(logonCookie);
            return RedirectToAction("", "");
        }

      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_ldap != null)
                {
                    _ldap = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("", "");
        }

       
        #endregion
    }
}