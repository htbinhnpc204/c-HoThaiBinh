namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblUserAccount")]
    public partial class tblUserAccount
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string userName { get; set; }

        [Required]
        [StringLength(50)]
        public string passWord { get; set; }

        public bool status { get; set; }

        [StringLength(255)]
        public string displayName { get; set; }
    }
}
