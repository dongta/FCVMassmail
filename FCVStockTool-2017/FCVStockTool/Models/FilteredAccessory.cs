using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCVStockTool
{
    public class FilteredAccessory
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }
        public string Name { get; set; }

        public int? Amount { get; set; }
    }
}