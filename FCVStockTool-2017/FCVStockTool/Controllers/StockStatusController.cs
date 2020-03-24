using FCVStockTool.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCVStockTool.Controllers
{
    public class StockStatusController:  CommonController
    {
        void BindList(int pageNo, List<StockStatus> cate)
        {
            if (cate != null)
            {
                ViewBag.StockStatuses = cate.OrderBy(p => p.Text).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = cate.Count();
            }
            else
            {
                ViewBag.StockStatuss = db.StockStatuses.OrderBy(p => p.Text).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.Categories.Count();
            }

            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Category
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {
            var cate = from s in db.StockStatuses
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                cate = cate.Where(s => s.Text.Contains(searchString));
            }

            var model = new StockStatus();
            if (id > 0) model = db.StockStatuses.Find(id);
            BindList(pageNo, cate.ToList());
            //ViewBag.Types = new SelectList(new[] { new { Id = 1, Text = "Nhập" }, new { Id = 2, Text = "Xuất" } }.ToList(), "Id", "Text", model.Type);
            return View(model);
        }


        // GET: Category/Create
        public ActionResult Create(StockStatus model, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                //model.Code = AutoNumberHelper.GenerateNumber(ObjectType.Category, db);
                db.StockStatuses.Add(model);
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: Category/Edit/5
        public ActionResult Edit(StockStatus model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: Category/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.StockStatuses.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }
	}
}