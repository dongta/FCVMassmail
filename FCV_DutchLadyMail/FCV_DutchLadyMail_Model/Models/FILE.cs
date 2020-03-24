namespace FCV_DutchLadyMail_Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FILES")]
    public partial class FILE
    {
        public int id { get; set; }

        public int userId { get; set; }

        public string path { get; set; }

        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public int? idFolders { get; set; }
        public virtual USER USER { get; set; }
    }
}
