using System;
using System.Collections.Generic;
using System.Text;

namespace UrlShortenerApp.Service.Abstractions
{
    public interface IAnalyticService
    {
        public Task LogAnalytic(string shortCode, string ipAddress, string userAgent);
    }
}
