namespace ModelEF.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HoThaiBinhDbContext : DbContext
    {
        public HoThaiBinhDbContext()
            : base("name=HoThaiBinhDbContext")
        {
        }

        public virtual DbSet<tblCategory> tblCategories { get; set; }
        public virtual DbSet<tblProduct> tblProducts { get; set; }
        public virtual DbSet<tblUserAccount> tblUserAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblProduct>()
                .Property(e => e.produnitCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tblUserAccount>()
                .Property(e => e.userName)
                .IsUnicode(false);

            modelBuilder.Entity<tblUserAccount>()
                .Property(e => e.passWord)
                .IsUnicode(false);
        }
    }
}
