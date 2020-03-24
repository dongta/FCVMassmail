using FCV_DutchLadyMail_Model.Attribute;
using FCV_DutchLadyMail_Model.Helpers;
using FCV_DutchLadyMail_Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Security.Authentication;
using System.Security.Cryptography;
using Newtonsoft.Json;
using FCV_DutchLadyMail.Object;
using FCV_DutchLadyMail_Model.Common;

namespace FCV_DutchLadyMail.Controllers
{
    //[Auth]
    public class UserController : BaseController
    {
        private MassMailsDbContext database = new MassMailsDbContext();
        // GET: Administrator/User
        public ActionResult Index()
        {
            return View(database.USERS.OrderBy(r => r.Username).ToList());
        }

        public ActionResult AddFromDomain(string paramJson)
        {
            List<USER> _lstU = new List<USER>();
            var lstUser = paramJson.Split('|');
            foreach (string user in lstUser)
            {
                var field = user.Split(';');
                string username = field[0];
                var ck = database.USERS.Where(ta => ta.Username.ToUpper().Equals(username.ToUpper())).Count();
                if (ck == 0)
                {
                    USER u = new USER();
                    u.Username = field[0];
                    u.DisplayName = field[1];
                    u.OU = field[2];
                    u.Email = field[3];
                    u.GUID = new Guid(field[4]);
                    database.USERS.Add(u);
                    database.SaveChanges();

                }

            }

            return Json(new { mess = "User list already added successfully !" });
        }
        public ViewResult Details(int id)
        {
            USER user = database.USERS.Find(id);
            SetViewBagData(id);
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(USER user)
        {
            if (user.Username == "" || user.Username == null)
            {
                ModelState.AddModelError(string.Empty, "Username cannot be blank");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> results = database.Database.SqlQuery<String>(string.Format("SELECT Username FROM USERS WHERE Username = '{0}'", user.Username)).ToList();
                    bool _userExistsInTable = (results.Count > 0);

                    USER _user = null;
                    if (_userExistsInTable)
                    {

                    }
                    else
                    {

                    }

                }
            }
            catch (Exception)
            {
                //return base.ShowError(ex);
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            USER user = database.USERS.Find(id);
            SetViewBagData(id);
            return View(user);
        }
        private void SetViewBagData(int _userId)
        {
            ViewBag.UserId = _userId;
            ViewBag.List_boolNullYesNo = StringHelper.List_boolNullYesNo();
            ViewBag.RoleId = new SelectList(database.ROLES.OrderBy(p => p.RoleName), "Role_Id", "RoleName");
        }

        [HttpGet]
        public ActionResult DeleteUserRole(int id, int userId)
        {
            ROLE role = database.ROLES.Find(id);
            USER user = database.USERS.Find(userId);

            if (role.USERS.Contains(user))
            {
                role.USERS.Remove(user);
                database.SaveChanges();
            }
            return RedirectToAction("Details", "USER", new { id = userId });
        }

        [HttpGet]
        public PartialViewResult filter4Users(string _surname)
        {
            return PartialView("_ListUserTable", GetFilteredUserList(_surname));
        }

        [HttpGet]
        public PartialViewResult filterReset()
        {
            return PartialView("_ListUserTable", database.USERS.ToList());
        }

        [HttpGet]
        public PartialViewResult DeleteUserReturnPartialView(int userId)
        {
            try
            {
                USER user = database.USERS.Find(userId);

                if (user != null)
                {
                    //database.USERS_ROLES.RemoveRange(database.ROLES.Contains(user).Where(ta => ta.User_Id == userId));
                    foreach (var item in user.ROLES.ToList())
                    {
                        user.ROLES.Remove(item);
                        database.SaveChanges();
                    }
                    database.USERS.Remove(database.USERS.Find(userId));
                    database.SaveChanges();


                }
            }
            catch
            {
            }
            return this.filterReset();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                USER user = database.USERS.Find(id);

                if (user != null)
                {
                    foreach (var item in user.ROLES.ToList())
                    {
                        user.ROLES.Remove(item);
                        database.SaveChanges();
                    }
                    database.USERS.Remove(database.USERS.Find(id));
                    database.SaveChanges();
                }
            }
            catch
            {
            }
            return RedirectToAction("Index"); ;
        }


        private IEnumerable<USER> GetFilteredUserList(string _surname)
        {
            IEnumerable<USER> _ret = null;
            try
            {
                if (string.IsNullOrEmpty(_surname))
                {
                    _ret = database.USERS.ToList();
                }
                else
                {
                    _ret = database.USERS.Where(p => p.DisplayName == _surname).ToList();
                }
            }
            catch
            {
            }
            return _ret;
        }

        public List<USER> getOU(string container, string name, string OU)
        {
            List<USER> _lstUser = new List<USER>();
            //string container = @"OU=Friesland Foods Dutch Lady Malaysia,DC=domaina,DC=int,DC=net";
            string strDomain = "domaina.int.net"; string strUserName = "crm.recording"; string strPassword = "P@$$w0rd";
            //string container = "OU=IVGHN,DC=ivg,DC=vn";
            var ctx1 = new PrincipalContext(ContextType.Domain, strDomain, container, strUserName, strPassword);
            UserPrincipal prUsr = new UserPrincipal(ctx1);
            prUsr.Name = "*";
            var searcher = new System.DirectoryServices.AccountManagement.PrincipalSearcher();
            searcher.QueryFilter = prUsr;
            var results = searcher.FindAll();
            int i = 1;
            if (!String.IsNullOrEmpty(name))
            {
                foreach (UserPrincipal p in results)
                {
                    if (p.SamAccountName.ToUpper().Contains(name.ToUpper()))
                    {
                        USER u = new USER();
                        u.Username = p.SamAccountName;
                        u.DisplayName = p.DisplayName;
                        u.Email = p.EmailAddress;
                        u.GUID = p.Guid.Value;

                        u.User_Id = i++;
                        _lstUser.Add(u);
                    }

                }
            }
            else
            {
                foreach (UserPrincipal p in results)
                {
                    USER u = new USER();
                    u.Username = p.SamAccountName;
                    u.DisplayName = p.DisplayName;
                    u.Email = p.EmailAddress;
                    u.GUID = p.Guid.Value;
                    u.User_Id = i++;
                    _lstUser.Add(u);
                }
            }
            return _lstUser;
        }
        public ActionResult GetListSearch(string name)
        {
            //string strDomain = "192.168.0.1"; string strUserName = "CrmInstall"; string strPassword = "ivgvn123$";
            string strDomain = "domaina.int.net"; string strUserName = "crm.recording"; string strPassword = "P@$$w0rd";
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, strDomain);
            List<USER> _lstUser = new List<USER>();
            try
            {
                bool bValid = ctx.ValidateCredentials(strUserName, strPassword, ContextOptions.Negotiate);

                // Additional check to search user in directory.
                if (bValid)
                {
                    //| OU=Friesland Foods Dutch Lady Malaysia OU=Friesland Foods Dutch Lady Vietnam,

                    string container = @"OU=Friesland Foods Dutch Lady Malaysia,DC=domaina,DC=int,DC=net";
                    //string container = "OU=IVGHN,DC=ivg,DC=vn";
                    _lstUser = getOU(container, name, "Friesland Foods Dutch Lady Malaysia");
                    string container1 = @"OU=Friesland Foods Dutch Lady VietNam,DC=domaina,DC=int,DC=net";
                    _lstUser.AddRange(getOU(container1, name, "Friesland Foods Dutch Lady VietNam"));

                }
            }
            catch (Exception ex)
            {
                throw new AuthenticationException("Authentication Error in PrincipalContext. Message: " + ex.Message);
            }
            finally
            {
                ctx.Dispose();
            }

            return PartialView("_ListUserTableDomain", _lstUser.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            database.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult DeleteUserRoleReturnPartialView(int id, int userId)
        {
            ROLE role = database.ROLES.Find(id);
            USER user = database.USERS.Find(userId);

            if (role.USERS.Contains(user))
            {
                role.USERS.Remove(user);
                database.SaveChanges();
            }
            SetViewBagData(userId);
            return PartialView("_ListUserRoleTable", database.USERS.Find(userId));
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult AddUserRoleReturnPartialView(int id, int userId)
        {
            ROLE role = database.ROLES.Find(id);
            USER user = database.USERS.Find(userId);

            if (!role.USERS.Contains(user))
            {
                role.USERS.Add(user);
                database.SaveChanges();
            }
            SetViewBagData(userId);
            return PartialView("_ListUserRoleTable", database.USERS.Find(userId));
        }

        [HttpPost]
        public JsonResult ChangePassword(Pass p)
        {
            IVGResult r = new IVGResult();
            try
            {
                if (string.IsNullOrEmpty(p.NewPass) || string.IsNullOrEmpty(p.ReNewPass))
                {
                    r.success = false;
                    r.Message = "All fields are required";
                    return Json(r);
                }
                if (p.NewPass != p.ReNewPass)
                {
                    r.success = false;
                    r.Message = "Confirm password not equal";
                    return Json(r);
                }
                var hashPassword = StringHelper.hasPasswordMD5(p.NewPass);
                IdentityUser iden = (IdentityUser)Session["Identity"];
                USER u = database.USERS.Where(a => a.Username == iden.UserName).FirstOrDefault();
                u.Password = hashPassword;
                database.SaveChanges();
                r.success = true;
                r.Message = "Your password has been changed successfully";
                return Json(r);
            }
            catch (Exception ex)
            {

                r.success = false;
                r.Message = ex.Message;
                return Json(r);
            }
        }
        [HttpPost]
        public JsonResult NewUser(NewUser u)
        {

            IVGResult r = new IVGResult();
            try
            {
                if (string.IsNullOrEmpty(u.UserName) || string.IsNullOrEmpty(u.DisplayName))
                {
                    r.success = false;
                    r.Message = "Please enter User Name and Display Name";
                    return Json(r);
                }

                if (database.USERS.Any(a => a.Username == u.UserName))
                {
                    r.success = false;
                    r.Message = "Someone already has that username. Try another?";
                    return Json(r);
                }
                var hashPassword = StringHelper.hasPasswordMD5("fcv@123a");
                USER newUser = new USER();
                newUser.Username = u.UserName;
                newUser.Password = hashPassword;
                newUser.Email = u.Email;
                newUser.DisplayName = u.DisplayName;
                database.USERS.Add(newUser);
                database.SaveChanges();
                r.success = true;
                r.Message = "User created successfully";
                return Json(r);
            }
            catch (Exception ex)
            {

                r.success = false;
                r.Message = ex.Message;
                return Json(r);
            }
        }
    }
}