
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCV_DutchLadyMail.Controllers
{
    public class UnauthorisedController : Controller
    {
        // GET: Administrator/Unauthorised
        public ActionResult Index()
        {
            return View();
        }
    }
}