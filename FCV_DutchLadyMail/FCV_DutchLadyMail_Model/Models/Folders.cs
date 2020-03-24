namespace FCV_DutchLadyMail_Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Folders")]
    public partial class Folders
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }

        public string Description { get; set; }

        public string created_at { get; set; }
        public string updated_at { get; set; }

        public string created_time { get; set; }
        public int? Cate { get; set; }
    }
}
