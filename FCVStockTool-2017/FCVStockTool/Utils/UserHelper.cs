using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCVStockTool.Utils
{
    public static class UserHelper
    {
        public static UserIndentity GetUser(this HttpRequestBase request)
        {
            UserIndentity user = new UserIndentity();
            HttpCookie logonCookie = request.Cookies["StockTool"];
            if (logonCookie != null)
                user.Username = logonCookie["Username"].ToString();
            user.DisplayName = logonCookie["DisplayName"].ToString();
            user.Id = int.Parse(logonCookie["UserID"].ToString());
            user.DepartmentId = int.Parse(logonCookie["DepartmentId"].ToString());
            user.RoleName = logonCookie["Role"].ToString();
            return user;
        }

    }

    public class UserIndentity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public int DepartmentId { get; set; }
        public string RoleName { get; set; }
    }

}
  