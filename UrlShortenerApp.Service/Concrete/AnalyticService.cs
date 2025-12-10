using UrlShortenerApp.Domain;
using UrlShortenerApp.Infrastructure.Abstractions;
using UrlShortenerApp.Service.Abstractions;

namespace UrlShortenerApp.Service.Concrete
{
    public class AnalyticService : IAnalyticService
    {
        public readonly IAnalyticRepository _analyticRepository;

        public AnalyticService(IAnalyticRepository analyticRepository)
        {
            _analyticRepository = analyticRepository;
        }
        public async Task LogAnalytic(string shortCode, string ipAddress, string userAgent)
        {
            var analytic = new Analytic(shortCode, userAgent, ipAddress);

            await _analyticRepository.Create(analytic);
        }
    }
}
