using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCV_DutchLadyMail_Model.Models;
using PagedList;
using System.Data.Entity;

namespace FCV_DutchLadyMail.Controllers
{
    public class RegionsController : BaseController
    {
        private MassMailsDbContext db = new MassMailsDbContext();
        //
        // GET: /Signatures/

        public ActionResult Index(string searchString, string currentFilter, int page = 1, int pageSize = 10)
        {
            var _list = db.Regions.OrderBy(ta=>ta.Name).ToList();
            return View(_list.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Region r)
        {

            if (r != null)
            {
                db.Regions.Add(r);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View();

        }

        public ActionResult Edit(int id)
        {

            var r = db.Regions.Where(m => m.ID == id).Single();
            return View(r);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Region r)
        {
            db.Entry(r).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            db.Regions.Remove(db.Regions.Find(id));
            db.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
