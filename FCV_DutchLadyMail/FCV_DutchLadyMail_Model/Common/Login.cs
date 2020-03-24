using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FCV_DutchLadyMail_Model.Common
{
    public class Login
    {
        [Required(ErrorMessage = "Username can not bank...!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password can not bank...!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}