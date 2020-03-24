using Excel;
using FCVStockTool.Filters;
using FCVStockTool.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCVStockTool.Controllers
{
    [Filters.AdminRole]
    //[AdminRole]
    public class UserController : Controller
    {
        StockDbContext db = new StockDbContext();
        Utils.LdapAuthentication _ldap = new Utils.LdapAuthentication(string.Format("LDAP://{0}", ConfigurationManager.AppSettings["Domain"]));
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

        void BindList(int pageNo, List<User> user)
        {
            if (user != null)
            {
                ViewBag.Users = user.OrderBy(p => p.DisplayName).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = user.Count();
            }
            else
            {
                ViewBag.Users = db.Users.OrderBy(p => p.DisplayName).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.Users.Count();
            }

            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: User
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {
            var user = from a in db.Users
                       select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                user = user.Where(a => a.DisplayName.ToLower().Contains(searchString.ToLower()) || a.Username.ToLower().Contains(searchString.ToLower()));
            }


            var model = new User();
            if (id > 0) model = db.Users.Find(id);
            BindList(pageNo, user.ToList());
            ViewBag.Departments = new SelectList(db.Departments.ToList(), "Id", "Name", model.DepartmentId);
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Id", "Name", model.RoleId);
            return View(model);
        }


        // GET: User/Create
        public ActionResult Create(User model, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: User/Edit/5
        public ActionResult Edit(User model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: User/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.Users.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        public JsonResult Import(HttpPostedFileBase file)
        {
            DataSet ds = file.ToDataSet();
            User item;
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        item = new User()
                        {
                            Username = r[0].ToString(),
                            DisplayName = r[1].ToString(),
                            Email = r[2].ToString()
                        };
                        db.Users.Add(item);
                    }
                }
                db.SaveChanges();
            }
            return Json(new { success = true });
        }

        public JsonResult FindADUserInfor(string userName)
        {
             ImpersonatorUser _user = _ldap.Finduser(userName);
            return Json(new { results=_user, success = true});
        }
    }
}
