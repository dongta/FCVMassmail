namespace FCVStockTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("StockDiaryReasons")]
    public partial class StockDiaryReason
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StockDiaryReason()
        {
            //StockDiaries = new HashSet<StockDiary>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Text { get; set; }
        [DefaultValue(0)]
        public int Type { get; set; }

        public string TypeName { get {
            if (Type == 1)
                return "Nhập";
            else
                return "Xuất";
        } }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StockDiary> StockDiaries { get; set; }
    }
}
