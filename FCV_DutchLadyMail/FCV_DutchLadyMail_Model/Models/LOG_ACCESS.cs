using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FCV_DutchLadyMail_Model.Models
{
    [Table("LOG_ACCESS")]
    public class LOG_ACCESS
    {

        public int id { get; set; }

        public int User_Id { get; set; }
        public string UserName { get; set; }

        public string page { get; set; }

        public bool? status { get; set; }

        public DateTime? created_at { get; set; }
        public virtual USER USER { get; set; }
    }
}