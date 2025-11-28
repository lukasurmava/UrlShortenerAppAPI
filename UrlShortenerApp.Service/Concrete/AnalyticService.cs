using System;
using System.Collections.Generic;
using System.Text;
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
            var analytic = new Analytic();
            analytic.ShortCode = shortCode;
            analytic.ClickDate = DateTime.UtcNow;
            analytic.IpAdress = ipAddress;
            analytic.UserAgent = userAgent;
            await _analyticRepository.Create(analytic);
        }
    }
}
