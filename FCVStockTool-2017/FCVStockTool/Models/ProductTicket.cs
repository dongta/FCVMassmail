namespace FCVStockTool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductTicket
    {
        public int Id { get; set; }

        [StringLength(15, ErrorMessageResourceType = typeof(Language.ProductTicket.ProTicket), ErrorMessageResourceName = "CodeError")]
        public string Code { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language.ProductTicket.ProTicket), ErrorMessageResourceName = "CategoryError")]
        public int CategoryId { get; set; }

        [StringLength(500, ErrorMessageResourceType = typeof(Language.ProductTicket.ProTicket), ErrorMessageResourceName = "ReasonError")]
        [Required(ErrorMessageResourceType = typeof(Language.ProductTicket.ProTicket), ErrorMessageResourceName = "ReasonError")]
        public string Reason { get; set; }

        public int? RequestById { get; set; }

        public DateTime? RequestOn { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language.ProductTicket.ProTicket), ErrorMessageResourceName = "EmployeeError")]
        public int EmployeeId { get; set; }

        public int? ApproveById { get; set; }

        public DateTime? ApproveOn { get; set; }

        public int? ExportById { get; set; }

        public DateTime? ExportOn { get; set; }

        public int? CancelById { get; set; }

        public DateTime? CancelOn { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(Language.ProductTicket.ProTicket), ErrorMessageResourceName = "SerialNoErrorLenght")]
        public string SerialNo { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public int? TicketStatusId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Category Category { get; set; }

        public virtual User RequestBy { get; set; }

        public virtual User ApproveBy { get; set; }

        public virtual User ExportBy { get; set; }

        public virtual User CancelBy { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
    }
}
