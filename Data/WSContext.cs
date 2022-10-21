using BitirmeProjesiErp.Models;
using Microsoft.EntityFrameworkCore;
namespace BitirmeProjesiErp.Data
{
    public class WSContext : DbContext
    {
        public WSContext(DbContextOptions<WSContext> options) : base(options)
        {
            // bu alanları açınca serverdaki bu contexte bağlı tüm tabloları silip tekrar oluşturur, tke tek elle silmek zor oluyor.
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        public DbSet<WSModel> WebServisBilgisi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WSModel>().ToTable("WebServisBilgi");
        }
        
    }
}
