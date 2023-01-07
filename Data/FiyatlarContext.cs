using BitirmeProjesiErp.Models;
using Microsoft.EntityFrameworkCore;
namespace BitirmeProjesiErp.Data
{
    public class FiyatlarContext : DbContext
    {
        public FiyatlarContext(DbContextOptions<FiyatlarContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }


        public DbSet<BitirmeProjesiErp.Models.Fiyat1> fiyat1 { get; set; }
        public DbSet<BitirmeProjesiErp.Models.Fiyat2> fiyat2 { get; set; }
        public DbSet<BitirmeProjesiErp.Models.Fiyat3> fiyat3 { get; set; }
        public DbSet<BitirmeProjesiErp.Models.Fiyat4> fiyat4 { get; set; }
        public DbSet<BitirmeProjesiErp.Models.Fiyat5> fiyat5 { get; set; }
        public DbSet<BitirmeProjesiErp.Models.Fiyat6> fiyat6 { get; set; }
        public DbSet<BitirmeProjesiErp.Models.Yedek1> yedek1 { get; set; }
        public DbSet<BitirmeProjesiErp.Models.Yedek2> yedek2 { get; set; }
        public DbSet<BitirmeProjesiErp.Models.Yedek3> yedek3 { get; set; }
        public DbSet<BitirmeProjesiErp.Models.Yedek4> yedek4 { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Teklif>().ToTable("Teklif");
            //modelBuilder.Entity<CariKart>().ToTable("CariKart");
            //modelBuilder.Entity<TeklifKalemi>().HasKey(x => x._key);
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Fiyat1>()
            .Property(p => p._key)
            .ValueGeneratedOnAdd();
            
        }
    }
}
