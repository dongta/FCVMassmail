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
    public class BuildingController : Controller
    {
        StockDbContext db = new StockDbContext();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

        void BindList(int pageNo)
        {
            ViewBag.Buildings = db.Buildings.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
            ViewBag.TotalRecords = db.Buildings.Count();
            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Building
        public ActionResult Index(int pageNo = 1, int id = 0)
        {
            var model = new Building();
            if (id > 0) model = db.Buildings.Find(id);
            BindList(pageNo);
            return View(model);
        }


        // GET: Building/Create
        public ActionResult Create(Building model, int pageNo = 1)
        {
            //model.Code = AutoNumberHelper.GenerateNumber(ObjectType.Building, db);
            db.Buildings.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: Building/Edit/5
        public ActionResult Edit(Building model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: Building/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.Buildings.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        public JsonResult Import(HttpPostedFileBase file)
        {
            DataSet ds = file.ToDataSet();
            Building item;
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        item = new Building()
                        {
                            Code = r[0].ToString(),
                            Name = r[1].ToString()
                        };
                        db.Buildings.Add(item);
                    }
                }
                db.SaveChanges();
            }
            return Json(new { success = true });
        }
    }
}
