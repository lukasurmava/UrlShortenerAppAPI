using Microsoft.EntityFrameworkCore;
using UrlShortenerApp.Domain;

namespace UrlShortenerApp.Infrastructure.Data
{
    public class UrlShortenerAppDbContext : DbContext
    {
        public UrlShortenerAppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OriginalUrl>().HasKey(s => s.ShortCode);
            builder.Entity<Analytic>().HasKey(a => a.Id);
            builder.Entity<OriginalUrl>().Property(o => o.ShortCode).HasMaxLength(8);
        }


        public DbSet<OriginalUrl> OriginalUrls { get; set; }
        public DbSet<Analytic> Analytics { get; set; }
    }
}
