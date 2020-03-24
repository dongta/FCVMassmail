namespace FCVStockTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            ProductTickets = new HashSet<ProductTicket>();
        }
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(250)]
        [Required(ErrorMessageResourceType = typeof(Language.Category.Category), ErrorMessageResourceName = "NameError")]
        public string Name { get; set; }

        public string Code { get; set; }

        [StringLength(250)]
        public string Position { get; set; }

        public string UserName { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Language.Employee.Employee), ErrorMessageResourceName = "DepError")]
        public int ?DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductTicket> ProductTickets { get; set; }
    }
}
