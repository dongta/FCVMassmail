namespace FCVStockTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InvoiceDetail
    {
        public int Id { get; set; }

        public int? InvoiceId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public int? WarrantyPeriod { get; set; }

        public int? WarrantyBrief { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual Product Product { get; set; }
    }
}
