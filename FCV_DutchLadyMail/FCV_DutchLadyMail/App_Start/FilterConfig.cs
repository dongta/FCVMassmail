﻿using System.Web;
using System.Web.Mvc;

namespace FCV_DutchLadyMail
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}