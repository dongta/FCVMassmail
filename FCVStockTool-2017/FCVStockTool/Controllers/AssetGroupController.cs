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
    public class AssetGroupController : Controller
    {
        StockDbContext db = new StockDbContext();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

        void BindList(int pageNo)
        {
            ViewBag.AssetGroups = db.AssetGroups.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
            ViewBag.TotalRecords = db.AssetGroups.Count();
            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: AssetGroup
        public ActionResult Index(int pageNo = 1, int id = 0)
        {
            var model = new AssetGroup();
            if (id > 0) model = db.AssetGroups.Find(id);
            BindList(pageNo);
            return View(model);
        }


        // GET: AssetGroup/Create
        public ActionResult Create(AssetGroup model, int pageNo = 1)
        {
            db.AssetGroups.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: AssetGroup/Edit/5
        public ActionResult Edit(AssetGroup model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: AssetGroup/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.AssetGroups.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        public JsonResult Import(HttpPostedFileBase file)
        {
            DataSet ds = file.ToDataSet();
            AssetGroup item;
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        item = new AssetGroup()
                        {
                            Name = r[1].ToString()
                        };
                        db.AssetGroups.Add(item);
                    }
                }
                db.SaveChanges();
            }
            return Json(new { success = true });
        }
    }
}
