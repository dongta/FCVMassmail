using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCV_DutchLadyMail_Model.Models;
using PagedList;
using System.Web.Routing;
using System.Data.Entity;
using FCV_DutchLadyMail_Model.Attribute;
using FCV_DutchLadyMail_Model.Common;


namespace FCV_DutchLadyMail.Controllers
{
    public class TemplatesController:BaseController
    {
        private MassMailsDbContext db = new MassMailsDbContext();
        //
        // GET: /Templates/

        public ActionResult Index(string searchString, string currentFilter,int page = 1, int pageSize = 10)
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
            var _list =  from l in db.Templates select l; 
            if (!String.IsNullOrEmpty(searchString))
            {
                _list = _list.Where(s => s.Name.Contains(searchString));
            }
            return View(_list.ToList().ToPagedList(page, pageSize));
        }

        public ActionResult Details(string id)
        {
            Templates temp = db.Templates.Find(int.Parse(id));

            return View("Details", temp);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Templates temMail)
        {

            if (temMail != null && ModelState.IsValid)
            {
                temMail.CreationTime = DateTime.Now.ToShortDateString();
                var Identity = (IdentityUser)Session["Identity"];
                temMail.CreatedBy = Identity.UserName;
                temMail.Active = true;
                db.Templates.Add(temMail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View();
            
        }
        public ActionResult Edit(int id)
        {

            var _temp = db.Templates.Where(m => m.ID == id).Single();
            return View(_temp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Templates _temp, bool? IsChecked)
        {
            if (ModelState.IsValid)
            {
                var Identity = (IdentityUser)Session["Identity"];
                _temp.UpdateTime = DateTime.Now.ToShortDateString();
                _temp.LastUpdatedBy = Identity.UserName;
                _temp.Active = true;
                //if (IsChecked == true)
                //    _temp.Active = true;
                //else
                //    _temp.Active = false;
                db.Entry(_temp).State = EntityState.Modified;
                db.SaveChanges();              
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Templates _temp = db.Templates.Find(id);
            if (_temp != null)
            {
                db.Templates.Remove(_temp);
                db.SaveChanges(); 
            }
            return RedirectToAction("Index");
        }

    }
}
