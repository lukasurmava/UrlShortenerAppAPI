using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortenerApp.Domain;
using UrlShortenerApp.Infrastructure.Abstractions;
using UrlShortenerApp.Infrastructure.Data;

namespace UrlShortenerApp.Infrastructure.Concrete
{
    public class AnalyticRepository : IAnalyticRepository
    {
        private readonly UrlShortenerAppDbContext _appDbContext;
        public AnalyticRepository(UrlShortenerAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Create(Analytic analytic)
        {
            _appDbContext.Add(analytic);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(string shortCode)
        {
            var analytic = GetByShortCode(shortCode);
            _appDbContext.Remove(analytic);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Analytic> GetByShortCode(string shortCode)
        {
            var analytic = await _appDbContext.Analytics.FirstOrDefaultAsync(s => s.ShortCode == shortCode);
            return analytic;
        }

        public async Task Update(Analytic analytic)
        {
            _appDbContext.Update(analytic);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
