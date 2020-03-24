using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCV_DutchLadyMail_Model.Common
{
    [Serializable]
    public class IdentityUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool isAdmin { get; set; }

    }
}