using Excel;
using FCVStockTool.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCVStockTool.Controllers
{
    [Filters.AdminRole]
    public class RoleController : Controller
    {
        StockDbContext db = new StockDbContext();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

        void BindList(int pageNo, List<Role> role)
        {
            if (role != null)
            {
                ViewBag.Roles = role.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = role.Count();
            }
            else
            {
                ViewBag.Roles = db.Roles.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.Roles.Count();
            }


            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Role
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {
            var role = from a in db.Roles
                       select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                role = role.Where(a => a.Name.Contains(searchString));
            }
            var model = new Role();
            if (id > 0) model = db.Roles.Find(id);
            BindList(pageNo, role.ToList());
            return View(model);
        }

        public ActionResult Permission(int id = 0)
        {
             return View();
        }


        // GET: Role/Create
        public ActionResult Create(Role model, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: Role/Edit/5
        public ActionResult Edit(Role model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: Role/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.Roles.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        public JsonResult Import(HttpPostedFileBase file)
        {
            DataSet ds = file.ToDataSet();
            Role item;
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        item = new Role()
                        {
                            Name = r[1].ToString(),
                            Description = r[2].ToString()
                        };
                        db.Roles.Add(item);
                    }
                }
                db.SaveChanges();
            }
            return Json(new { success = true });
        }
    }
}
