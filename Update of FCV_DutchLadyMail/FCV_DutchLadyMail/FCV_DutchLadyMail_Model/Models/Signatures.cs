using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Web.Script.Serialization;

namespace FCV_DutchLadyMail_Model.Models
{
    [Table("tbl_Signatures")]
    public class Signatures
    {

        public Nullable<int> ID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
        public string UpdateTime { get; set; }
        public Nullable<bool> State { get; set; }


          

    }
}