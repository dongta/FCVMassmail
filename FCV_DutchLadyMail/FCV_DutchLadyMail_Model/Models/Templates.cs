using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace FCV_DutchLadyMail_Model.Models
{
    [Table("tbl_Templates")]
    public class Templates
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên mail mẫu !")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập nội dung mail mẫu !")]
        public string Contents { get; set; }
        public string CreatedBy { get; set; }
        public string CreationTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public string UpdateTime { get; set; }
        public bool? Active { get; set; }
    }
}