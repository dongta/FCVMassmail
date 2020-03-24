using FCVStockTool.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FCVStockTool.Controllers
{
    [Filters.AccountantRole]
    public class InvoiceController : Controller
    {
        StockDbContext db = new StockDbContext();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

        void BindList(int pageNo, List<Invoice> invoices)
        {
            if (invoices != null)
            {
                ViewBag.Invoices = invoices.OrderByDescending(p => p.PODate).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = invoices.Count();
            }
            else
            {
                ViewBag.Invoices = db.Invoices.OrderByDescending(p => p.PODate).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.Invoices.Count();
            }
            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Invoice
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {

            var invoices = from s in db.Invoices
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                invoices = invoices.Where(s => s.PONo.Contains(searchString));
            }

            var model = new Invoice();
            if (id > 0) model = db.Invoices.Find(id);
            BindList(pageNo, invoices.ToList());
            return View(model);
        }

        public ActionResult Details(int id = 0)
        {
            var model = new Invoice();
            if (id > 0) model = db.Invoices.Find(id);
            ViewBag.Suppliers = new SelectList(db.Suppliers.ToList(), "Id", "Name", model.SupplierId);
            ViewBag.Products = new SelectList(db.Products.ToList(), "Id", "Name");
            return View(model);
        }


        // GET: Invoice/Create
        public ActionResult Create(Invoice model)
        {
          
                db.Invoices.Add(model);
                db.SaveChanges();
     
            return RedirectToAction("Details", "invoice", new { id = model.Id });
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(Invoice model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", "invoice", new { id = model.Id });
        }



        // GET: Invoice/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.Invoices.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }


    }
}
