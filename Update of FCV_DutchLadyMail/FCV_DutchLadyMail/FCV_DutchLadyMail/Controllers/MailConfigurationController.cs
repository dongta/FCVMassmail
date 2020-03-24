using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCV_DutchLadyMail_Model.Models;
using PagedList;
using System.Data.Entity;
using FCV_DutchLadyMail_Model.Attribute;
using FCV_DutchLadyMail_Model.Common;
using FCV_DutchLadyMail_Model.Helpers;

namespace FCV_DutchLadyMail.Controllers
{
    public class MailConfigurationController : BaseController
    {
        //
        // GET: /MailConfiguration/
        private MassMailsDbContext db = new MassMailsDbContext();
        public ActionResult Index()
        {
            var config = db.SMTPProfiles.ToList();
            return View(config);
        }

        public ActionResult ViewPass(string pass)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Index(MailConfig r)
        {
            db.Entry(r).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            int idC = Convert.ToInt32(id);
            SMTPProfiles temp = db.SMTPProfiles.Where(ta => ta.ID == idC).SingleOrDefault();

            return View("Details", temp);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(SMTPProfiles config, bool? IsChecked, bool? IsCheckedSSL)
        {
            if (ModelState.IsValid)
            {
                if (config != null)
                {
                    if (IsChecked == true)
                    {

                        config.Active = true;
                        var update = db.SMTPProfiles.Where(ta => ta.Active == true).ToList();
                        foreach (var u in update)
                        {
                            u.Active = false;
                            db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    else
                        config.Active = false;
                    if (IsCheckedSSL == true)
                        config.SSLEnable = true;
                    else
                        config.SSLEnable = false;
                    //config.Password = StringHelper.hasPasswordMD5(config.Password);
                    db.SMTPProfiles.Add(config);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }
            return View();

        }

        public ActionResult Edit(int id)
        {

            var config = db.SMTPProfiles.Where(m => m.ID == id).Single();
            return View(config);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(SMTPProfiles smtp, bool? IsChecked, bool? IsCheckedSSL)
        {
            if (ModelState.IsValid)
            {
                if (IsChecked == true)
                {

                    smtp.Active = true;
                    var update = db.SMTPProfiles.Where(ta => ta.Active == true).ToList();
                    foreach (var u in update)
                    {
                        u.Active = false;
                        db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                    smtp.Active = false;
                if (IsCheckedSSL == true)
                    smtp.SSLEnable = true;
                else
                    smtp.SSLEnable = false;
                db.Entry(smtp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            db.SMTPProfiles.Remove(db.SMTPProfiles.Find(id));
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
