using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCVStockTool.Models
{
    public class FilteredStockDiary
    {
        public int Id { get; set; }
        
        public DateTime? Date { get; set; }

        public string ShortCode { get; set; }

        public string Type { get; set; }

        public string Note { get; set; }

        public int? ProductItemId { get; set; }

        public int? ProductTicketId { get; set; }
    }
}