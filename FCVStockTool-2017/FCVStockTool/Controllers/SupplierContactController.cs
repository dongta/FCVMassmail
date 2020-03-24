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
    public class SupplierContactController : Controller
    {
        StockDbContext db = new StockDbContext();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

        void BindList(int pageNo, int supplierid, List<SupplierContact> supcon)
        {
            if (supcon != null)
            {
                //var supplier = db.SupplierContacts.AsQueryable();
                //if (supplierid != 0) supplier = supplier.Where(p => p.SupplierId == supplierid);
                ViewBag.SupplierContacts = supcon.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = supcon.Count();
            }
            else
            {
                //var supplier = db.SupplierContacts.AsQueryable();
                //if (supplierid != 0) supplier = supplier.Where(p => p.SupplierId == supplierid);
                //ViewBag.SupplierContacts = supplier.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                //ViewBag.TotalRecords = supplier.Count();
                ViewBag.SupplierContacts = db.SupplierContacts.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.SupplierContacts.Count();
            }

            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: SupplierContact
        public ActionResult Index(int pageNo = 1, int id = 0, int supplierid = 0, string searchString = "")
        {
            var sup = db.Suppliers.Where(a => a.Name.Contains(searchString)).FirstOrDefault();
            var supcon = from a in db.SupplierContacts
                         select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                supcon = db.SupplierContacts.Where(s => s.SupplierId.Equals(sup.Id));
            }


            var model = new SupplierContact();
            if (id > 0) model = db.SupplierContacts.Find(id);
            BindList(pageNo, supplierid, supcon.ToList());
            ViewBag.Suppliers = new SelectList(db.Suppliers.ToList(), "Id", "Name", supplierid);
            return View(model);
        }


        // GET: SupplierContact/Create
        public ActionResult Create(SupplierContact model, int pageNo = 1)
        {
            db.SupplierContacts.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: SupplierContact/Edit/5
        public ActionResult Edit(SupplierContact model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: SupplierContact/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.SupplierContacts.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        public JsonResult Import(HttpPostedFileBase file)
        {
            DataSet ds = file.ToDataSet();
            SupplierContact item;
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        item = new SupplierContact()
                        {
                            Name = r[1].ToString()
                        };
                        db.SupplierContacts.Add(item);
                    }
                }
                db.SaveChanges();
            }
            return Json(new { success = true });
        }
    }
}
