namespace FCVStockTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StockDiary
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public int? ProductItemId { get; set; }

        public string Type { get; set; }

        public string Note { get; set; }

        public int? ProductTicketId { get; set; }

        //public virtual ProductItem ProductItem { get; set; }

        //public virtual ProductTicket ProductTicket { get; set; }
    }
}
