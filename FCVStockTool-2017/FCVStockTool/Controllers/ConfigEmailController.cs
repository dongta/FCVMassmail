using FCVStockTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCVStockTool.Controllers
{
    [Filters.AdminRole]
    public class ConfigEmailController : Controller
    {
        StockDbContext db = new StockDbContext();

        //
        // GET: /ConfigEmail/
        public ActionResult Index()
        {
            var model = db.ConfigEmails.First();
            return View(model);
        }
        public ActionResult Create(ConfigEmail model) 
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
	}
}