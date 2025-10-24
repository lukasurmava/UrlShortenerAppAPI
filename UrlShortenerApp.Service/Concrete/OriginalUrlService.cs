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
                response.Error = "Expiredate can't be less then current date";
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

            response.OriginalUrl = originalUrl.OriginalLink;
            response.ShortUrl = originalUrl.ShortCode;
            response.CreatedOn = originalUrl.CreatedOn;
            response.ExpirationDate = originalUrl.ExpirationDate;
            response.ClickCount = originalUrl.ClickCount;
            response.IsSuccess = true;
            return response;
        }

        public async Task<DeleteOriginalUrlResponse> DeleteUrl(string shortCode)
        {
            var response = new DeleteOriginalUrlResponse();
            if (await _originalUrlRepository.GetByShortCode(shortCode) is not null)
            {
                await _originalUrlRepository.Delete(shortCode);
                response.IsSuccess = true;
                return response;
            }
            else
            {
                response.IsSuccess = false;
                response.Error = "Can't find record by this short code";
                return response;
            }
            
        }

        //This is redirect method
        public async Task GetByShortCode(string shortCode)
        {
            throw new NotImplementedException();
        }

        //This is method do get all the details
        public async Task<GetUrlDetailsResponse> GetUrlDetails(string shortCode)
        {
            var response = new GetUrlDetailsResponse();
            var originalUrl = await _originalUrlRepository.GetByShortCode(shortCode);
            var analytics = await _analyticRepository.GetByShortCode(shortCode);
            if (originalUrl is null)
            {
                response.IsSuccess = false;
                response.Error = "Can't find record by that short code";
            }
            else
            {
                response.IsSuccess = true;
                response.ShortCode = shortCode;
                response.ExpirationDate = originalUrl.ExpirationDate;
                response.CreatedOn = originalUrl.CreatedOn;
                response.Analytics = analytics;
                response.ClickCount = originalUrl.ClickCount;
                response.OriginalUrl = originalUrl.OriginalLink;
                return response;
            }

        }

        public async Task<UpdateOriginalUrlResponse> UpdateUrl(UpdateOriginalUrlRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
