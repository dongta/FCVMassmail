using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FCV_DutchLadyMail_Model.Models
{
    public class SendTemplateModel
    {
        [Required]
        [Display(Name = "Template")]
        public string SelectedTemplate { get; set; }
        public IEnumerable<SelectListItem> Templates { get; set; }
        [Required]
        public string resourceFile { get; set; }
    }
}