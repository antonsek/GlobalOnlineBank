
using GlobalOnlinebank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlobalOnlinebank.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contragent> Contragents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contragent>().HasKey(p => p.Id);

            // Seed data
            modelBuilder.Entity<Contragent>().HasData(
                new Contragent("ТОО Колбаса","ТОО Колбаса","LLP Kolbasa",  "961545123222") { Id = -1, CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc)},
                new Contragent("ТОО Рахат","ТОО Рахат","LLP Rahat",  "961545123251") { Id = -2, CreatedDate = new DateTime(2024, 11, 8, 0, 0, 0, DateTimeKind.Utc) }
            );
        }
    }
}