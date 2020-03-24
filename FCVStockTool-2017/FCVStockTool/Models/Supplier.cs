namespace FCVStockTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            Invoices = new HashSet<Invoice>();
            SupplierContacts = new HashSet<SupplierContact>();
        }

        public int Id { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(250)]
        [Required(ErrorMessageResourceType = typeof(Language.Category.Category), ErrorMessageResourceName = "NameError")]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(15)]
        public string Fax { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(20)]
        public string Website { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierContact> SupplierContacts { get; set; }
    }
}
