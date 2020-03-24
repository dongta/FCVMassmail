using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCV_DutchLadyMail_Model.Models
{
    public class SendListMails
    {
        public string ID
        {
            get;
            set;
        }
        public string Code
        {
            get;
            set;
        }
        public string From
        {
            get;
            set;
        }
        public string To
        {
            get;
            set;
        }
        public string Cc
        {
            get;
            set;
        }
        public string Bcc
        {
            get;
            set;
        }
        public string Files
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
    }
}