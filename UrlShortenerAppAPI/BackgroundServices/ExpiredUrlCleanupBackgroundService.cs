using UrlShortenerApp.Service.Abstractions;

namespace UrlShortenerApp.API.BackgroundServices
{
    public class ExpiredUrlCleanupBackgroundService : BackgroundService
    {
        private readonly IOriginalUrlService _originalUrlService;
        private readonly ILogger<ExpiredUrlCleanupBackgroundService> _logger;
        public ExpiredUrlCleanupBackgroundService(IOriginalUrlService originalUrlService, ILogger<ExpiredUrlCleanupBackgroundService> logger)
        {
            _originalUrlService = originalUrlService;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _originalUrlService.DeleteExpiredUrl();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting expired URLs.");
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

    }
}
