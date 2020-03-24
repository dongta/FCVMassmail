using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FCVStockTool.Models
{
    public partial class AccountantAsset
    {
        public int Id { get; set; }
        public string Group { get; set; }

        [StringLength(50)]
        public string Class { get; set; }
        public int FixedAssetCode { get; set; }
        public string FixedAssetName { get; set; }
        public int CostCenter { get; set; }
        public int UsefulLife { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DepreciationDate { get; set; }

        [DataType(DataType.Currency)]
        public Nullable<Decimal> Acquisition { get; set; }

        [StringLength(30)]
        public string Sub_code { get; set; }

        [StringLength(50)]
        public string Description_subCode { get; set; }

        [StringLength(50)]
        public string Barcode { get; set; }

        [DataType(DataType.Currency)]
        public Nullable<Decimal> Acquisition_subcode { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RetirementDate { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(30)]
        public string Owner { get; set; }
        public string Remarks { get; set; }
    }
}