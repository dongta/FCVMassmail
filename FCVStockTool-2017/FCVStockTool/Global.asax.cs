using FCVStockTool.App_Start;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace FCVStockTool
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            // Manually installed WebAPI 2.2 after making an MVC project.
            GlobalConfiguration.Configure(WebApiConfig.Register); // NEW way
            //WebApiConfig.Register(GlobalConfiguration.Configuration); // DEPRECATED
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string culture = "vi";//ngon ngu mac dinh
            var httpCookie = Request.Cookies["language"];
            if (httpCookie != null)
            {
                culture = httpCookie.Value;
            }
            else
            {
                HttpCookie language = new HttpCookie("language");
                language.Value = culture;
                language.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(language);
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
        }
    }
}
