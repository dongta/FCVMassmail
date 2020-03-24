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
    [Filters.AccountantRole]
    public class AssetClassController : Controller
    {
        StockDbContext db = new StockDbContext();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

        void BindList(int pageNo)
        {
            ViewBag.AssetClasses = db.AssetClasses.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
            ViewBag.TotalRecords = db.AssetClasses.Count();
            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: AssetClass
        public ActionResult Index(int pageNo = 1, int id = 0)
        {
            var model = new AssetClass();
            if (id > 0) model = db.AssetClasses.Find(id);
            BindList(pageNo);
            return View(model);
        }


        // GET: AssetClass/Create
        public ActionResult Create(AssetClass model, int pageNo = 1)
        {
            db.AssetClasses.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: AssetClass/Edit/5
        public ActionResult Edit(AssetClass model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: AssetClass/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.AssetClasses.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        public JsonResult Import(HttpPostedFileBase file)
        {
            DataSet ds = file.ToDataSet();
            AssetClass item;
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        item = new AssetClass()
                        {
                            Name = r[1].ToString()
                        };
                        db.AssetClasses.Add(item);
                    }
                }
                db.SaveChanges();
            }
            return Json(new { success = true });
        }
    }
}
