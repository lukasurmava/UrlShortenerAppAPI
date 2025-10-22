using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
