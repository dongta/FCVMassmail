using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCV_DutchLadyMail_Model.Models
{
    public class MailConfig
    {
        [Key]
        public int ID
        {
            get;
            set;
        }
        public string User
        {
            get;
            set;
        }
        public string Pass
        {
            get;
            set;
        }
        public string IP
        {
            get;
            set;
        }
        public string Protocol
        {
            get;
            set;
        }
        public string Port
        {
            get;
            set;
        }
    }
}