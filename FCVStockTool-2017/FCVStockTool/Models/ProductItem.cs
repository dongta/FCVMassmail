namespace FCVStockTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductItem
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language.ProductItem.ProItem), ErrorMessageResourceName = "ProductError")]
        public int ProductId { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(Language.ProductItem.ProItem), ErrorMessageResourceName = "SerialErrorLenght")]
        [Required(ErrorMessageResourceType = typeof(Language.ProductItem.ProItem), ErrorMessageResourceName = "SerialNoError")]
        public string SerialNo { get; set; }
        
        [StringLength(20)]
        [Required(ErrorMessageResourceType = typeof(Language.ProductItem.ProItem), ErrorMessageResourceName = "ShortCodeError")]
        public string ShortCode { get; set; }

        //[StringLength(20)]
        //[Required(ErrorMessageResourceType = typeof(Language.ProductItem.ProItem), ErrorMessageResourceName = "NameError")]
        public string Name { get; set; }

        public int? InvoiceId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Language.ProductItem.ProItem), ErrorMessageResourceName = "StockStatusError")]
        public int? StockStatusId { get; set; }
        
        //[Required(ErrorMessageResourceType = typeof(Language.ProductItem.ProItem), ErrorMessageResourceName = "ProductStatusError")]
        public int? ProductStatusId { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? EmployeeId { get; set; }

        public int? SupplierId { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual Product Product { get; set; }

        public virtual ProductStatus ProductStatus { get; set; }

        public virtual StockStatus StockStatus { get; set; }
        public virtual Employee Employee { get; set; }

        public int? BuildingId { get; set; }

        public virtual Building Building { get; set; }

        public virtual Supplier Supplier { get; set; }

        public string PONo { get; set; }
        public DateTime? PODate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        
        public int? CDROM { get; set; }

        public int? DepartmentId { get; set; }
    }
}
