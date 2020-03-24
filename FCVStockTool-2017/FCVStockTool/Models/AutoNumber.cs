namespace FCVStockTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AutoNumber
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Object { get; set; }

        public int? Year { get; set; }

        public int? Number { get; set; }
    }
}
