using FCVStockTool.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCVStockTool.Controllers
{
    public class ProductStatusController : CommonController
    {
        void BindList(int pageNo, List<ProductStatus> cate)
        {
            if (cate != null)
            {
                ViewBag.ProductStatuses = cate.OrderBy(p => p.Text).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = cate.Count();
            }
            else
            {
                ViewBag.ProductStatuss = db.ProductStatuses.OrderBy(p => p.Text).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.Categories.Count();
            }

            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Category
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {
            var cate = from s in db.ProductStatuses
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                cate = cate.Where(s => s.Text.Contains(searchString));
            }

            var model = new ProductStatus();
            if (id > 0) model = db.ProductStatuses.Find(id);
            BindList(pageNo, cate.ToList());
            //ViewBag.Types = new SelectList(new[] { new { Id = 1, Text = "Nhập" }, new { Id = 2, Text = "Xuất" } }.ToList(), "Id", "Text", model.Type);
            return View(model);
        }


        // GET: Category/Create
        public ActionResult Create(ProductStatus model, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                //model.Code = AutoNumberHelper.GenerateNumber(ObjectType.Category, db);
                db.ProductStatuses.Add(model);
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: Category/Edit/5
        public ActionResult Edit(ProductStatus model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: Category/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.ProductStatuses.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }
	}
}