using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCV_DutchLadyMail_Model.Models;
using PagedList;


namespace FCV_DutchLadyMail.Controllers
{
    public class HomeController : BaseController
    {
        private MassMailsDbContext db = new MassMailsDbContext();
        //
        // GET: /Home/

        public ActionResult Index(int page = 1, int pageSize = 2)
        {
            var _list = db.Templates.ToList().ToPagedList(page, pageSize);
            return View(_list);
        }

    }
}
