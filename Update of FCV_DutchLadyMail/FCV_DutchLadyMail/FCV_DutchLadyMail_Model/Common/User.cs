using FCV_DutchLadyMail_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCV_DutchLadyMail_Model.Common
{
    public class User
    {
        MassMailsDbContext db = null;
        public User()
        {
            db = new MassMailsDbContext();
        }
        public USER Login(string UserName, string Password, bool isLoginAdmin = false)
        {
            return db.USERS.SingleOrDefault(x => x.Username == UserName);
        }

    }
}