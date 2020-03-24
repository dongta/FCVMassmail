using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCV_DutchLadyMail_Model.Models
{
    public class DistributorsView
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
        public Nullable<bool> State { get; set; }
        public Region Regions { get; set; }
    }
}