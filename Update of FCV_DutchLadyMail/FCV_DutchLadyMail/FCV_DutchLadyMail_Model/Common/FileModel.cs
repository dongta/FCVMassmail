using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FCV_DutchLadyMail_Model.Common
{
    public class FileModel
    {
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] files { get; set; }

        [Display(Name = "Folder")]
        public string folder { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}