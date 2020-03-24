namespace FCVStockTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            ProductTicketRequestBys = new HashSet<ProductTicket>();
            ProductTicketApproveBys = new HashSet<ProductTicket>();
            ProductTicketExportBys = new HashSet<ProductTicket>();
        }

        public int Id { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Language.User.User), ErrorMessageResourceName = "UsernameErrorLenght")]
        [Required(ErrorMessageResourceType = typeof(Language.User.User), ErrorMessageResourceName = "UsernameError")]
        public string Username { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Language.User.User), ErrorMessageResourceName = "DisplayNameErrorLenght")]
        [Required(ErrorMessageResourceType = typeof(Language.User.User), ErrorMessageResourceName = "DisplayNameError")]
        public string DisplayName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [DefaultValue(true)]
        public bool Active { get; set; }

        public int? RoleId { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Role Role { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductTicket> ProductTicketRequestBys { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductTicket> ProductTicketApproveBys { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductTicket> ProductTicketExportBys { get; set; }
        public virtual ICollection<ProductTicket> ProductTicketCancelBys { get; set; }
    }
}
