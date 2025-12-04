using UrlShortenerApp.Service.Abstractions;

namespace UrlShortenerApp.API.BackgroundServices
{
    public class ExpiredUrlCleanupBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ExpiredUrlCleanupBackgroundService> _logger;
        public ExpiredUrlCleanupBackgroundService(IServiceScopeFactory scopeFactory, ILogger<ExpiredUrlCleanupBackgroundService> logger)
        {
            _serviceScopeFactory = scopeFactory;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var urlService = scope.ServiceProvider.GetRequiredService<IOriginalUrlService>();
                    await urlService.DeleteExpiredUrls();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting expired URLs.");
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

    }
}
