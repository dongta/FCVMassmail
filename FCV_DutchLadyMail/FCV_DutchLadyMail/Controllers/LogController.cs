using FCV_DutchLadyMail_Model.Attribute;
using FCV_DutchLadyMail_Model.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCV_DutchLadyMail.Controllers
{
    public class LogController : BaseController
    {
        private MassMailsDbContext database = new MassMailsDbContext();
        // GET: Log
        public ActionResult Index(int? page)
        {
            int pageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["pageSize"]);
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            List<LOG_ACCESS> _logs = database.LOG_ACCESS
                              .OrderBy(wn => wn.created_at)
                              .ToList();
            return View(_logs.ToPagedList(pageIndex, pageSize));
        }
        [Auth]
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Delete(int id)
        {
            LOG_ACCESS log = database.LOG_ACCESS.Find(id);
            database.Entry(log).State = EntityState.Deleted;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}