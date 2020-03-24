using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FCVStockTool.Models
{
    public partial class ConfigEmail
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType=typeof(Language.ConfigEmail.ConfigEmail),ErrorMessageResourceName="HostErorr")]
        public string Host { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        public string EmailFrom { get; set; }
    }
}