namespace FCVStockTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SupplierContact
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        public int? SupplierId { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
