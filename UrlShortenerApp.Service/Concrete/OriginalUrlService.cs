using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortenerApp.Infrastructure.Abstractions;
using UrlShortenerApp.Infrastructure;
using UrlShortenerApp.Service.Abstractions;
using UrlShortenerApp.Service.Requests;
using UrlShortenerApp.Service.Responses;
using UrlShortenerApp.Domain;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace UrlShortenerApp.Service.Concrete
{
    public class OriginalUrlService : IOriginalUrlService
    {
        private readonly IAnalyticRepository _analyticRepository;
        private readonly IOriginalUrlRepository _originalUrlRepository;
        public OriginalUrlService(IAnalyticRepository analyticRepository, IOriginalUrlRepository originalUrlRepository)
        {
            _analyticRepository = analyticRepository;
            _originalUrlRepository = originalUrlRepository;
        }
        public async Task<CreateOriginalUrlResponse> CreateShortUrl(CreateOriginalUrlRequest request)
        {
            var response = new CreateOriginalUrlResponse();
            if (request.ExpireDate < DateTime.UtcNow)
            {
                response.IsSuccess = false;
                response.Error = "Expiredate can't be in the past";
                return response;
            }
            var shortcode = request.Alias ?? Infrastructure.Common.GenerateShortCode();
            var originalUrl = new OriginalUrl()
            {
                ShortCode = shortcode,
                CreatedOn = DateTime.UtcNow,
                ClickCount = 0,
                ExpirationDate = request.ExpireDate,
                IsActive = true, 
                OriginalLink = request.OriginalUrl
            };
            await _originalUrlRepository.Create(originalUrl);
            var dbOriginalUrl = await _originalUrlRepository.GetByShortCode(shortcode);
            return new CreateOriginalUrlResponse()
            {
                OriginalUrl = dbOriginalUrl.OriginalLink,
                ShortUrl = dbOriginalUrl.ShortCode,
                CreatedOn = dbOriginalUrl.CreatedOn,
                ExpirationDate = dbOriginalUrl.ExpirationDate,
                ClickCount = dbOriginalUrl.ClickCount
            };
            
        }

        public async Task DeleteUrl(string shortCode)
        {
            throw new NotImplementedException();
        }

        public async Task GetByShortCode(string shortCode)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUrlDetailsResponse> GetUrlDetails(string shortCode)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUrl(UpdateOriginalUrlRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
