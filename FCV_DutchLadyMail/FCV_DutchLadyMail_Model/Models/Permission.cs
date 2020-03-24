namespace FCV_DutchLadyMail_Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PERMISSIONS")]
    public partial class PERMISSION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PERMISSION()
        {
            ROLES = new HashSet<ROLE>();
        }

        [Key]
        public int Permission_Id { get; set; }

        [StringLength(200)]
        public string PermissionName { get; set; }

        [StringLength(200)]
        public string PermissionDescription { get; set; }

        public virtual ICollection<ROLE> ROLES { get; set; }
    }
}
