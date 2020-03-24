namespace FCVStockTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
            ProductItems = new HashSet<ProductItem>();
            ChildProduct = new HashSet<Product>();
           
            //StockDiaries = new HashSet<StockDiary>();
        }

        public int Id { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(100)]
        [Required(ErrorMessageResourceType = typeof(Language.Product.Product), ErrorMessageResourceName = "NameError")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? Type { get; set; }

        public int? ParentId { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Language.Product.Product), ErrorMessageResourceName = "CategoryError")]
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductItem> ProductItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> ChildProduct { get; set; }

        public virtual Product Parent { get; set; }
    }
}
