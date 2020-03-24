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
    public class StockDiaryController : Controller
    {
        StockDbContext db = new StockDbContext();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

        void BindList(int pageNo, string searchString)
        {
            var pro = db.FilteredStockDiaries.AsQueryable();
            if (searchString != string.Empty)pro = pro.Where(p => p.ShortCode.ToLower().Contains(searchString.ToLower()));
            ViewBag.StockDiaries = pro.OrderByDescending(p => p.Date).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
            ViewBag.TotalRecords = pro.Count();
            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: StockDiary
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {
            BindList(pageNo, searchString);
            return View();
        }
       
        [HttpGet]
        public JsonResult GetStockDiary(int productItemId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.StockDiaries.Where(p=>p.ProductItemId == productItemId).OrderByDescending(p=>p.Date).AsEnumerable();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
