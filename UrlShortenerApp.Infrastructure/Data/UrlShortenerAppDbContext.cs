using Microsoft.EntityFrameworkCore;
using UrlShortenerApp.Domain;

namespace UrlShortenerApp.Infrastructure.Data
{
    public class UrlShortenerAppDbContext : DbContext
    {
        public UrlShortenerAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<OriginalUrl> OriginalUrls { get; set; }
        public DbSet<Analytic> Analytics { get; set; }
    }
}
