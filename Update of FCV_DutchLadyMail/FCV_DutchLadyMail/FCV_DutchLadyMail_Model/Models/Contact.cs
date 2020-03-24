using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Spatial;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace FCV_DutchLadyMail_Model.Models
{
    [Table("tbl_Contacts")]
    public class Contact
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Remarks { get; set; }
        public string Company { get; set; }
        
        public int ContactListID { get; set; }
        [ForeignKey("ContactListID")]
        public virtual ContactLists CONTACTLISTs { get; set; }

        
    }
}