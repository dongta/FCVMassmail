using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FCVStockTool.Models
{
    public partial class ReceiveEmail
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "nhập name")]
        [Required(ErrorMessageResourceType = typeof(Language.ReceiveEmail.ReceiveEmail), ErrorMessageResourceName = "NameError")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language.ReceiveEmail.ReceiveEmail), ErrorMessageResourceName = "EmailError")]
        [EmailAddress(ErrorMessageResourceType = typeof(Language.ReceiveEmail.ReceiveEmail), ErrorMessageResourceName = "EmailErrorType",ErrorMessage="")]
        public string Email { get; set; }
    }
}