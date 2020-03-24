using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Spatial;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCV_DutchLadyMail_Model.Models
{
    

    [Table("tbl_ContactLists")]
    public class ContactLists
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }

        public DateTime? CreationTime { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public string LastImportFileName { get; set; }
        public string Remarks { get; set; }

        public virtual ICollection<Contact> CONTACTs { get; set; }

    }
}