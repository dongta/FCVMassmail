using FCVStockTool.Models;
using FCVStockTool.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCVStockTool.Controllers
{
    [Filters.AdminRole]
    public class ReceiveEmailController : Controller
    {
        //
        // GET: /ReceiveEmail/
        StockDbContext db = new StockDbContext();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

        void BindList(int pageNo, List<ReceiveEmail> reEmail)
        {
            if (reEmail != null)
            {
                ViewBag.ReceiveEmail = reEmail.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = reEmail.Count();
            }
            else
            {
                ViewBag.ReceiveEmail = db.ReceiveEmails.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.ReceiveEmails.Count();
            }

            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {
            //Search
            var reEmail = from a in db.ReceiveEmails
                          select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                reEmail = reEmail.Where(a => a.Name.Contains(searchString));
            }
            //End Search

            var model = new ReceiveEmail();
            if (id > 0) model = db.ReceiveEmails.Find(id);

            //truyền reEmail vào BindList
            BindList(pageNo, reEmail.ToList());
            return View(model);
        }

        // GET: ReceiveEmail/Create
        public ActionResult Create(ReceiveEmail model, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                db.ReceiveEmails.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { PageNo = pageNo });
        }
        // GET: ReceiveEmail/Edit/5
        public ActionResult Edit(ReceiveEmail model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        // GET: ReceiveEmail/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.ReceiveEmails.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }
    }
}