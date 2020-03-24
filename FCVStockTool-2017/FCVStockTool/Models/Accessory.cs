using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FCVStockTool
{
    public class Accessory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language.Accessory.Accessory), ErrorMessageResourceName = "CategoryError")]
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        
        [StringLength(250)]
        [Required(ErrorMessageResourceType = typeof(Language.Accessory.Accessory), ErrorMessageResourceName = "NameError")]
        public string Name { get; set; }
        [Range(typeof(Int32), "0", "100000", ErrorMessageResourceType = typeof(Language.Accessory.Accessory), ErrorMessageResourceName = "AmountError")]
        public int? Amount { get; set; }

        public int? ImportByUserId { get; set; }
        public virtual User ImportByUser { get; set; }
        public DateTime? ImportOn { get; set; }

        public string Description { get; set; }
    }
}