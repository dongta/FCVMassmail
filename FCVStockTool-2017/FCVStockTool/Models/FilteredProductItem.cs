using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FCVStockTool.Models
{
    [Table("dbo.FilteredProductItems")]
    public class FilteredProductItem
    {
         [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
          public string ShortCode { get; set; }
          public string Name { get; set; }
          public string CSGName { get; set; }
          public string ProductClassName { get; set; }
          public string ProductName { get; set; }
          public string SectionName { get; set; }
          public string BuildingName { get; set; }
          public string UserName { get; set; }  
          public string DisplayName { get; set; }
          public string Department { get; set; }
          public string StatusName { get; set; }
          public string SerialNo { get; set; }
          public DateTime? ExpiryDate { get; set; }
          public DateTime? AcquiredDate { get; set; }
          public string SupplierName { get; set; }
          public string PONo { get; set; }
          public string Description { get; set; }
    }
}