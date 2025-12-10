using UrlShortenerApp.Domain;

namespace UrlShortenerApp.Infrastructure.Abstractions
{
    public interface IAnalyticRepository
    {
        public Task Create(Analytic analytic);

        public Task<List<Analytic>> GetByShortCode(string shortCode);
        public Task Update(Analytic analytic);
        public Task Delete(string shortCode);
    }
}
