using CarDealerWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealerWebApp.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) :DbContext(options)
    {
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Inquiry>Inquiries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
