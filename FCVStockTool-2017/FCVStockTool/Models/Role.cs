namespace FCVStockTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Language.Role.Role), ErrorMessageResourceName = "NameErrorLenght")]
        [Required(ErrorMessageResourceType = typeof(Language.Role.Role), ErrorMessageResourceName = "NameError")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessageResourceType = typeof(Language.Role.Role), ErrorMessageResourceName = "DescriptionErrorLenght")]
        public string Description { get; set; }

        [DefaultValue(false)]
        public bool Active { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
