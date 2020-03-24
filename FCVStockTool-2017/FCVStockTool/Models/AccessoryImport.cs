using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FCVStockTool
{
    public class AccessoryImport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Language.AccessoryExport.AccessoryExport), ErrorMessageResourceName = "AccessoryError")]
        public int? AccessoryId { get; set; }

        public virtual Accessory Accessory { get; set; }
        [Required(ErrorMessageResourceType = typeof(Language.AccessoryExport.AccessoryExport), ErrorMessageResourceName = "AmountRqError")]
        [Range(typeof(Int32), "0", "100000", ErrorMessageResourceType = typeof(Language.AccessoryExport.AccessoryExport), ErrorMessageResourceName = "AmountError")]
        public int? Amount { get; set; }
        [Required(ErrorMessageResourceType = typeof(Language.AccessoryExport.AccessoryExport), ErrorMessageResourceName = "EmpError")]

        public int? ImportByUserId { get; set; }

        public virtual User ImportByUser { get; set; }

        public DateTime? ImportOn { get; set; }
        [DefaultValue(false)]
        public bool? Cancel { get; set; }

        public string Description { get; set; }
    }
}