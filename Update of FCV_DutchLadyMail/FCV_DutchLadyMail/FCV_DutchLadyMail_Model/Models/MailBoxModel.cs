using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FCV_DutchLadyMail_Model.Models
{
     public class MailBoxModel
    {
        [Display(Name ="To")]
        public string ToEmail { get; set; }
        [Display(Name = "From")]
        public string FromEmail { get; set; }
        [Display(Name = "Subject")]
        public string EmailSubject { get; set; }
    }
}