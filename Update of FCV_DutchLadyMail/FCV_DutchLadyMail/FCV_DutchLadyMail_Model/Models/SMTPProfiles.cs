using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.ComponentModel.DataAnnotations;

namespace FCV_DutchLadyMail_Model.Models
{
    [Table("tbl_SMTPProfiles")]
    public class SMTPProfiles
    {
        public int ID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string SMTPServer
        {
            get;
            set;
        }
        public int? SMTPPort
        {
            get;
            set;
        }
        public string Account
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }
        public bool? SSLEnable
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }

        public int? MaxTries
        {
            get;
            set;
        }
        public int? TryInterval
        {
            get;
            set;
        }
        public bool? Active
        {
            get;
            set;
        }
    }
}