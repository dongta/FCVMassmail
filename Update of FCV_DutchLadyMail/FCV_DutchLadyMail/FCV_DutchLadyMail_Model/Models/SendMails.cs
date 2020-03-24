using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.ComponentModel.DataAnnotations;

namespace FCV_DutchLadyMail_Model.Models
{
    [Table("tbl_SentEmails")]
    public class SendMails
    {
        [Key]
        public int ID
        {
            get;
            set;
        }
        public string DistributorCode
        {
            get;
            set;
        }
        public int? ContactListID
        {
            get;
            set;
        }
        public int? SMTPProfile
        {
            get;
            set;
        }
        public string fromAddress
        {
            get;
            set;
        }
        public string fromName
        {
            get;
            set;
        }
        public string toAddress
        {
            get;
            set;
        }
        public string ccAddress
        {
            get;
            set;
        }
        public string bccAddress
        {
            get;
            set;
        }
        public string Subject
        {
            get;
            set;
        }
        public string Body
        {
            get;
            set;
        }

        public int? Template
        {
            get;
            set;
        }
        public DateTime? QueueTime
        {
            get;
            set;
        }
        public DateTime? SentTime
        {
            get;
            set;
        }
        public string SentBy
        {
            get;
            set;
        }

        public string AttachedFolder
        {
            get;
            set;
        }
        public string AttachedFiles
        {
            get;
            set;
        }
        public string InlineFolder
        {
            get;
            set;
        }
        public string InlineFiles
        {
            get;
            set;
        }
        public DateTime? FirstTry
        {
            get;
            set;
        }
        public DateTime? LastTry
        {
            get;
            set;
        }
        public int? Tries
        {
            get;
            set;
        }


        //public SendMails() {}
        //public SendMails(QueueMails q)
        //{

        //    this.fromAddress = q.fromAddress;
        //    this.fromName = q.fromName;
        //    this.toAddress = q.toAddress;
        //    this.ccAddress = q.ccAddress;
        //    this.bccAddress = q.bccAddress;
        //    this.Subject = q.Subject;
        //    this.Body = q.Body;
        //    this.QueueTime = q.QueueTime;
        //    this.SentBy = q.SentBy;
        //}
    }
}