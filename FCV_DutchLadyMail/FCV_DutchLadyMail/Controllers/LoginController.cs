using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCV_DutchLadyMail_Model.Helpers;
using FCV_DutchLadyMail_Model.Common;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Security.Authentication;
using System.Security.Cryptography;
using FCV_DutchLadyMail_Model.Models;
using System.Configuration;

namespace FCV_DutchLadyMail.Controllers
{
    
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        private MassMailsDbContext db = new MassMailsDbContext();
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index(string returnUrl = "")
        {
            var Identity = (IdentityUser)Session["Identity"];
            if (Identity != null && Identity.UserId > 0)
                return RedirectToAction("Index");
            var login = new Login();
            ViewBag.ReturnUrl = returnUrl;
            return View("", login);
        }

        [HttpPost]
        public ActionResult Index(Login login, string returnUrl = "")
        {
            string error = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    string authenticationMode = ConfigurationManager.AppSettings["AuthenticationMode"];
                    if (authenticationMode != null && authenticationMode.ToLower() == "db")
                    {
                        var hashPassword = StringHelper.hasPasswordMD5(login.Password);
                        var user = db.USERS.Where(a => a.Username == login.UserName && a.Password == hashPassword).FirstOrDefault();

                        if (user != null)
                        {
                            IdentityUser _identity = new IdentityUser();
                            _identity.UserId = user.User_Id;
                            _identity.UserName = user.Username;
                            _identity.FullName = user.DisplayName;
                            _identity.Address = null;
                            _identity.Email = user.Email;
                            Session.Add("Identity", _identity);

                            if (Url.IsLocalUrl(returnUrl))
                                return Redirect(returnUrl);
                            else
                                return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("LoginValidate", "Username or password wrong! Please check again!");
                        }
                    }
                    else
                    {
                        IdentityUser _identity = new IdentityUser();
                        try
                        {
                            _identity = AuthenticateUsingPrincipalcontext("domaina.int.net", login.UserName, login.Password);
                            //_identity = AuthenticateUsingPrincipalcontext("192.168.0.1", login.UserName, login.Password);
                            if (_identity.UserName != null)
                            {
                                Session.Add("Identity", _identity);


                                if (Url.IsLocalUrl(returnUrl))
                                    return Redirect(returnUrl);
                                else
                                    return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                error = "8";
                                ModelState.AddModelError("LoginValidate", "Username or password wrong! Please check again!");
                            }

                            //string strMessage = String.Format("User '{0}' authenticated successfully with PrincipalContext, \nDistinguished Name: {1}", login.UserName, strDistinguishedName);
                        }
                        catch (AuthenticationException ex)
                        {
                            //ModelState.AddModelError("LoginValidate", ex.Message);
                            string strMessage = String.Format("Error: {0}", ex.Message);
                        }
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return Content("ERROR: --> " + ex.Message + error);
            }
        }
        public IdentityUser Login(Login login)
        {
            IdentityUser _Identity = new IdentityUser();
            if (ModelState.IsValid)
            {
                var user = new User();
                var hashPassword = StringHelper.hasPasswordMD5(login.Password);
                var result = user.Login(login.UserName, hashPassword);
                if (result != null)
                {
                    _Identity = new IdentityUser();
                    _Identity.UserId = result.User_Id;
                    _Identity.UserName = result.Username;
                    _Identity.FullName = result.DisplayName;
                    _Identity.Address = "admin@gmail.com";
                    foreach (ROLE _role in result.ROLES)
                    {
                        _Identity.isAdmin = _role.IsSysAdmin;
                    }

                    Session.Add("Identity", _Identity);

                }
                else
                {
                    ModelState.AddModelError("", "Username or password wrong! Please check again!");
                }
            }
            return _Identity;
        }
        private IdentityUser AuthenticateUsingPrincipalcontext(string strDomain, string strUserName, string strPassword)
        {
            IdentityUser _Identity = new IdentityUser();
            var ck = db.USERS.Where(m => m.Username.Equals(strUserName)).Count();
            if (ck > 0)
            {
                PrincipalContext ctx = new PrincipalContext(ContextType.Domain, strDomain);

                try
                {
                    bool bValid = ctx.ValidateCredentials(strUserName, strPassword, ContextOptions.Negotiate);

                    // Additional check to search user in directory.
                    if (bValid)
                    {
                        var ctx1 = new PrincipalContext(ContextType.Domain, strDomain, strUserName, strPassword);
                        UserPrincipal prUsr = new UserPrincipal(ctx1);
                        prUsr.SamAccountName = strUserName;

                        PrincipalSearcher srchUser = new PrincipalSearcher(prUsr);
                        UserPrincipal foundUser = srchUser.FindOne() as UserPrincipal;

                        if (foundUser != null)
                        {

                            _Identity = new IdentityUser();
                            var u = db.USERS.Where(m => m.Username.Equals(foundUser.SamAccountName)).SingleOrDefault();
                            if (u != null)
                            {
                                _Identity.UserId = u.User_Id;
                                _Identity.UserName = foundUser.SamAccountName;
                                _Identity.FullName = foundUser.Name;
                                _Identity.Address = foundUser.EmailAddress;
                                _Identity.Email = foundUser.EmailAddress;
                            }

                        }
                        else
                            throw new AuthenticationException("Please enter valid UserName/Password.");
                    }
                    else
                        throw new AuthenticationException("Please enter valid UserName/Password.");

                }
                catch (Exception ex)
                {
                    throw new AuthenticationException("Authentication Error in PrincipalContext. Message: " + ex.Message);
                }
                finally
                {
                    ctx.Dispose();
                }

                
            }
            return _Identity;
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            // Redirecting to Login page after deleting Session
            return RedirectToAction("Index", "Login");
        }




    }
}
