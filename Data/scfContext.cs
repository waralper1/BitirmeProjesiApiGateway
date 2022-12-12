using BitirmeProjesiErp.Models;
using Microsoft.EntityFrameworkCore;
namespace BitirmeProjesiErp.Data
{
    public class scfContext : DbContext
    {
        public scfContext(DbContextOptions<scfContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }


        public DbSet<BitirmeProjesiErp.Models.Teklif> Teklifs { get; set; }
        public DbSet<BitirmeProjesiErp.Models.CariKart> CariKarts { get; set; }
        public DbSet<BitirmeProjesiErp.Models.SatisElemanlari> SatisElemanlaris { get; set; }
        public DbSet<BitirmeProjesiErp.Models.StokKart> StokKarts { get; set; }
        public DbSet<BitirmeProjesiErp.Models.TeklifKalemi> TeklifKalemis { get; set; }
        public DbSet<BitirmeProjesiErp.Models.TeklifViewModel> TeklifViewModel { get; set; }
        public DbSet<BitirmeProjesiErp.Models.OdemePlani> OdemePlanis { get; set; }
        public DbSet<BitirmeProjesiErp.Models.CariKartAdresler> CariKartAdreslers { get; set; }
        public DbSet<BitirmeProjesiErp.Models.Doviz> Dovizs { get; set; }
        public DbSet<BitirmeProjesiErp.Models.CariKartYetkili> CariKartYetkilis { get; set; }
        public DbSet<BitirmeProjesiErp.Models.Rpr_dinamik_raporparametreleri_getir> rpr_dinamik_raporparametreleri_getir { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Teklif>().ToTable("Teklif");
            //modelBuilder.Entity<CariKart>().ToTable("CariKart");
            //modelBuilder.Entity<TeklifKalemi>().HasKey(x => x._key);
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TeklifKalemi>()
            .Property(p => p._key)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<Teklif>()
            .Property(p => p._key)
            .ValueGeneratedOnAdd();
        }
    }
}
