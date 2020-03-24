using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCV_DutchLadyMail_Model.Models;
using PagedList;
using System.Data.Entity;
using System.Net.Mail;
using FCV_DutchLadyMail_Model.Attribute;
using FCV_DutchLadyMail_Model.Common;

namespace FCV_DutchLadyMail.Controllers
{

    public class SignaturesController : BaseController
    {
        private MassMailsDbContext db = new MassMailsDbContext();
        //
        // GET: /Signatures/

        public ActionResult Index(string searchString, string currentFilter, int page = 1, int pageSize = 10)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var _list = from l in db.Signatures select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                _list = _list.Where(s => s.Name.Contains(searchString));
            }
            return View(_list.ToList().ToPagedList(page, pageSize));
        }

        public ActionResult Details(string id)
        {
            Signatures temp = db.Signatures.Find(int.Parse(id));

            return View("Details", temp);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Signatures sig)
        {

            if (sig != null && ModelState.IsValid)
            {
                var Identity = (IdentityUser)Session["Identity"];
                sig.UserID = Identity.UserName;
                sig.UpdateTime = DateTime.Now.ToString();
                db.Signatures.Add(sig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View();

        }

        public ActionResult Edit(int id)
        {

            var sig = db.Signatures.Where(m => m.ID == id).Single();
            return View(sig);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Signatures sig)
        {
            sig.UpdateTime = DateTime.Now.ToString();
            db.Entry(sig).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            db.Signatures.Remove(db.Signatures.Find(id));
            db.SaveChanges(); 
            
            return RedirectToAction("Index");
        }


    }
}
