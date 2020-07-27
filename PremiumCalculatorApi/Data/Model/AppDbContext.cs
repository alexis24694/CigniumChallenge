using Microsoft.EntityFrameworkCore;

namespace PremiumCalculatorApi.Data.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<PremiumRule> PremiumRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PremiumRule>().ToTable("PremiumRule");
        }        
    }
}
