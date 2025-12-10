using Microsoft.EntityFrameworkCore;
using UrlShortenerApp.Domain;
using UrlShortenerApp.Infrastructure.Abstractions;
using UrlShortenerApp.Infrastructure.Data;

namespace UrlShortenerApp.Infrastructure.Concrete
{
    public class OriginalUrlRepository : IOriginalUrlRepository
    {
        private readonly UrlShortenerAppDbContext _appDbContext;

        public OriginalUrlRepository(UrlShortenerAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Create(OriginalUrl originalUrl)
        {
            _appDbContext.Add(originalUrl);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<OriginalUrl> GetByShortCode(string shortCode)
        {
            var originalUrl = await _appDbContext.OriginalUrls.FirstOrDefaultAsync(s => s.ShortCode == shortCode);
            return originalUrl;
        }

        public async Task Update(OriginalUrl originalUrl)
        {
            _appDbContext.Update(originalUrl);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(string shortCode)
        {
            _appDbContext.Remove(GetByShortCode(shortCode));
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<OriginalUrl>> GetAll()
        {
            var originalUrls = await _appDbContext.OriginalUrls.ToListAsync();
            return originalUrls;
        }

        public async Task Deactivate(string shortCode)
        {
            var originalUrl = await _appDbContext.OriginalUrls.FirstOrDefaultAsync(s => s.ShortCode == shortCode);
            originalUrl.SetIsActive(false);
            await _appDbContext.SaveChangesAsync();

        }

        public async Task<bool> ShortCodeExist(string shortCode)
        {
            var exists = await _appDbContext.OriginalUrls.AnyAsync(s => s.ShortCode == shortCode);
            return exists;
        }

        public async Task<List<OriginalUrl>> GetExpiredUrls()
        {
            var expiredUrls = await _appDbContext.OriginalUrls.Where(url => url.ExpirationDate <= DateTime.UtcNow).ToListAsync();
            return expiredUrls;
        }

    }
}
