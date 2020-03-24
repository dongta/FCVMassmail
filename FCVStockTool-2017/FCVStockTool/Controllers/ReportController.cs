using FCVStockTool.Models;
using FCVStockTool.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCVStockTool.Services;

namespace FCVStockTool.Controllers
{ [Filters.AdminRole]
    public class ReportController : CommonController
    {
        void BindList(int pageNo, List<Report> Report)
        {
            if (Report != null)
            {
                ViewBag.Reports = Report.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = Report.Count();
            }
            else
            {
                ViewBag.Reports = db.Reports.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.Reports.Count();
            }


            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Report
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {
            var Report = from a in db.Reports
                       select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                Report = Report.Where(a => a.Name.Contains(searchString));
            }
            var model = new Report();
            if (id > 0) model = db.Reports.Find(id);
            BindList(pageNo, Report.ToList());
            return View(model);
        }


        // GET: Report/Create
        public ActionResult Create(Report model, HttpPostedFileBase reportFile, int pageNo = 1)
        {
            Guid _guid = new Guid();
            ReportService rs = new ReportService();
            rs.SaveReport(model.Name, reportFile.InputStream, model.Name, true, ref _guid);
            model.GUID = _guid;
            if (ModelState.IsValid)
            {
                db.Reports.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: Report/Edit/5
        public ActionResult Edit(Report model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: Report/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.Reports.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }
    }
}
