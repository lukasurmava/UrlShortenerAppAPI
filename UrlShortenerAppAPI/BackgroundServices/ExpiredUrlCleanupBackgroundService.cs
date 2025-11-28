using UrlShortenerApp.Service.Abstractions;

namespace UrlShortenerApp.API.BackgroundServices
{
    public class ExpiredUrlCleanupBackgroundService : BackgroundService
    {
        private readonly IOriginalUrlService _originalUrlService;
        private readonly ILogger<ExpiredUrlCleanupBackgroundService> _logger;
        public ExpiredUrlCleanupBackgroundService(
    IOriginalUrlService originalUrlService,
    ILogger<ExpiredUrlCleanupBackgroundService> logger)
        {
            _originalUrlService = originalUrlService;
            _logger = logger;
        }
    }
}
