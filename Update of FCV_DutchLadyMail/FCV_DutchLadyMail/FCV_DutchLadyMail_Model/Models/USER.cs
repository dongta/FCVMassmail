namespace FCV_DutchLadyMail_Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbl_Users")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            LOG_ACCESS = new HashSet<LOG_ACCESS>();
            ROLES = new HashSet<ROLE>();
            FILES = new HashSet<FILE>();
        }

        [Key]
        public int User_Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        public string OU { get; set; }
        public string Email { get; set; }
        public Guid GUID { get; set; }
        public string Password { get; set; }

        public string Remarks { get; set; }
        public virtual ICollection<LOG_ACCESS> LOG_ACCESS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROLE> ROLES { get; set; }
        public virtual ICollection<FILE> FILES { get; set; }
    }
}
