﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FCV_DutchLadyMail_Model.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NhaPhanPhoi
    {
        [Key]
        public int NPP_ID { get; set; }
        public string NPPName { get; set; }
        public string Region { get; set; }
        public string ManagerMail { get; set; }
        public string AdminMail { get; set; }
        public string SEMail { get; set; }
        public string AcMail { get; set; }
        public string Adress { get; set; }
        public Nullable<bool> State { get; set; }
    }
}
