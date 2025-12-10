using UrlShortenerApp.Domain;

namespace UrlShortenerApp.Infrastructure.Abstractions
{
    public interface IOriginalUrlRepository
    {
        public Task Create(OriginalUrl originalUrl);
        public Task<OriginalUrl> GetByShortCode(string shortCode);
        public Task Update(OriginalUrl originalUrl);
        public Task Delete(string shortCode);
        public Task<List<OriginalUrl>> GetAll();
        public Task Deactivate(string shortCode);
        public Task<bool> ShortCodeExist(string shortCode);
        public Task<List<OriginalUrl>> GetExpiredUrls();
    }
}
