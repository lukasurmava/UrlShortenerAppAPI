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
            builder.Entity<OriginalUrl>().Property(o => o.ShortCode).HasMaxLength(8);
            builder.Entity<OriginalUrl>().Property(o => o.OriginalLink).HasMaxLength(2048);
            builder.Entity<OriginalUrl>().Property(u => u.IsActive).HasDefaultValue(true);
            builder.Entity<Analytic>().Property(a => a.UserAgent).HasMaxLength(512);
            builder.Entity<Analytic>().Property(a => a.IpAdress).HasMaxLength(45);
            builder.Entity<Analytic>().Property(a => a.ShortCode).HasMaxLength(8);
            builder.Entity<Analytic>().HasKey(a => a.Id);
        }


        public DbSet<OriginalUrl> OriginalUrls { get; set; }
        public DbSet<Analytic> Analytics { get; set; }
    }
}
