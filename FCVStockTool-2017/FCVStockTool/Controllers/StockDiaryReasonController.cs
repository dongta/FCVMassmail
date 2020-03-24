using FCVStockTool.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCVStockTool.Controllers
{
    public class StockDiaryReasonController : CommonController
    {
        public JsonResult GetByType(int type=0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.StockDiaryReasons.Where(s => s.Type == type).AsEnumerable();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insert(StockDiary model)
        {
            //model.Code = AutoNumberHelper.GenerateNumber(ObjectType.StockDiary, db);
            db.StockDiaries.Add(model);
            db.SaveChanges();
            return Json(new { Succeed= true});
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var model = db.StockDiaries.Find(id);
            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Json(new { Succeed = true });
        }

        void BindList(int pageNo, List<StockDiaryReason> cate)
        {
            if (cate != null)
            {
                ViewBag.StockDiaryReasons = cate.OrderBy(p => p.Text).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = cate.Count();
            }
            else
            {
                ViewBag.StockDiaryReasons = db.StockDiaryReasons.OrderBy(p => p.Text).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.Categories.Count();
            }

            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Category
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {
            var cate = from s in db.StockDiaryReasons
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                cate = cate.Where(s => s.Text.Contains(searchString));
            }

            var model = new StockDiaryReason();
            if (id > 0) model = db.StockDiaryReasons.Find(id);
            BindList(pageNo, cate.ToList());
            ViewBag.Types = new SelectList(new []{  new {Id=1, Text= "Nhập"}, new {Id=2, Text= "Xuất"}}.ToList(), "Id", "Text", model.Type);
            return View(model);
        }


        // GET: Category/Create
        public ActionResult Create(StockDiaryReason model, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                //model.Code = AutoNumberHelper.GenerateNumber(ObjectType.Category, db);
                db.StockDiaryReasons.Add(model);
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: Category/Edit/5
        public ActionResult Edit(StockDiaryReason model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: Category/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.StockDiaryReasons.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }
	}
}