﻿using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FCVStockTool.Utils
{
    public static class EnumHelper
    {
        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].CustomAttributes;

                if (attrs != null && attrs.Count() > 0)
                {
                    return ((System.ComponentModel.DescriptionAttribute)(((System.Attribute[])(memInfo[0].GetCustomAttributes()))[0])).Description;
                }
            }

            return en.ToString();
        }

    }

}