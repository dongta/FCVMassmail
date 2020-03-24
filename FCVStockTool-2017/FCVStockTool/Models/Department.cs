namespace FCVStockTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Code { get; set; }
        [StringLength(500)]
        [Required(ErrorMessageResourceType = typeof(Language.Department.Department), ErrorMessageResourceName = "NameError")]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
