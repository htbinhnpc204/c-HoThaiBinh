namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblProduct")]
    public partial class tblProduct
    {
        [Key]
        public int prodID { get; set; }

        [Required]
        [StringLength(100)]
        public string prodName { get; set; }

        public int? categoryID { get; set; }

        [Column(TypeName = "money")]
        public decimal produnitCost { get; set; }

        public int prodQuantity { get; set; }

        [Column(TypeName = "image")]
        public byte[] prodImage { get; set; }

        [StringLength(255)]
        public string prodDescription { get; set; }

        public bool prodStatus { get; set; }

        public virtual tblCategory tblCategory { get; set; }
    }
}
