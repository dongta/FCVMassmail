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
    [Table("tbl_Distributors")]
    public class DistributorModel
    {

        public Nullable<int> ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Region { get; set; }
        //public IEnumerable<SelectListItem> Regions { get; set; }
        public string AdminMail { get; set; }
        public string SEMail { get; set; }
        public string AcMail { get; set; }
        public string ManagerMail { get; set; }
        public bool? State { get; set; }


          

    }
}