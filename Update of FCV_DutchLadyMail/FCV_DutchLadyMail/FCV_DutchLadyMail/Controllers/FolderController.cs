using FCV_DutchLadyMail_Model.Common;
using FCV_DutchLadyMail_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
using System.Data.Entity;
using FCV_DutchLadyMail_Model.Attribute;

namespace FCV_DutchLadyMail.Controllers
{
    //[Auth]
    public class FolderController : BaseController
    {
        private MassMailsDbContext database = new MassMailsDbContext();
        // GET: File
        public ActionResult Index()
        {
            List<Folders> _folder = database.Folders.Where(ta=>ta.Cate != 2)
                              .OrderByDescending(wn => wn.created_at)
                              .ToList();
            return View(_folder.ToList());
        }
        
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Delete(int id)
        {
            Folders _folder = database.Folders.Find(id);
            database.Entry(_folder).State = EntityState.Deleted;
            database.SaveChanges();
            //string path =  _folder.Name + "/";
            //if (System.IO.File.Exists(Server.MapPath(path)))
            //{
            //    DirectoryInfo dir = new DirectoryInfo(Server.MapPath(path));
            //    dir.GetFiles("*", SearchOption.AllDirectories).ToList().ForEach(file => file.Delete());
            //    Directory.Delete(Server.MapPath(_folder.Name),true);
            //}
            return RedirectToAction("Index");
        }
        public ActionResult CheckPath(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                return Json(new { success = true, message = "The folder already exists!" }, JsonRequestBehavior.AllowGet);
            }

            else
                return Json(new { success = true, message = "The folder does not exist !" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Folders f)
        {

            if (f != null)
            {
                var Identity = (IdentityUser)Session["Identity"];
                string path = f.Name;
                //bool exists = System.IO.Directory.Exists(Server.MapPath(path));
                //if (!exists)
                //    System.IO.Directory.CreateDirectory(Server.MapPath(path));
                f.created_at = Identity.UserName;
                f.created_time = DateTime.Now.ToShortDateString();
                database.Folders.Add(f);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View();

        }

        public ActionResult Edit(int id)
        {

            var f = database.Folders.Where(m => m.ID == id).Single();
            return View(f);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Folders f)
        {

            f.updated_at = DateTime.Now.ToShortDateString();
            database.Entry(f).State = EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
