namespace FCV_DutchLadyMail_Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USER_ROLE")]
    public partial class USERs_ROLEs
    {

        [Key, Column(Order = 1)]
        public int User_Id { get; set; }
        [Key, Column(Order = 2)]
        public int Role_Id { get; set; }
    }
}
