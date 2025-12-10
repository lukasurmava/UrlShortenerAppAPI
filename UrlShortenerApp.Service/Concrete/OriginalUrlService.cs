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
        private readonly IAnalyticService _analyticService;
        public OriginalUrlService(IAnalyticRepository analyticRepository, IOriginalUrlRepository originalUrlRepository, IAnalyticService analyticService)
        {
            _analyticRepository = analyticRepository;
            _originalUrlRepository = originalUrlRepository;
            _analyticService = analyticService;
        }
        //Create ShortUrl
        public async Task<CreateOriginalUrlResponse> CreateShortUrl(CreateOriginalUrlRequest request)
        {
            var response = new CreateOriginalUrlResponse();
            string shortcode;
            if (request.ExpireDate < DateTime.UtcNow)
            {
                response.IsSuccess = false;
                response.Error = "Expiredate can't be less then current date";
                return response;
            }
            if (!string.IsNullOrEmpty(request.Alias))
            {
                var existingUrl = await _originalUrlRepository.ShortCodeExist(request.Alias);
                if (existingUrl)
                {
                    response.IsSuccess = false;
                    response.Error = "Alias is already in use. Please choose another one.";
                    return response;
                }
                shortcode = request.Alias;
            }
            else
            {
                shortcode = Common.GenerateShortCode();
                while (await _originalUrlRepository.ShortCodeExist(shortcode))
                {
                    shortcode = Common.GenerateShortCode();
                }
            }
            if (!Common.IsValidUrl(request.OriginalUrl))
            {
                response.IsSuccess = false;
                response.Error = "The provided URL is not valid.";
                return response;
            }

            var originalUrl = new OriginalUrl(shortcode, request.OriginalUrl, request.ExpireDate);

            await _originalUrlRepository.Create(originalUrl);

            response.OriginalUrl = originalUrl.OriginalLink;
            response.ShortUrl = originalUrl.ShortCode;
            response.CreatedOn = originalUrl.CreatedOn;
            response.ExpirationDate = originalUrl.ExpirationDate;
            response.ClickCount = originalUrl.ClickCount;
            response.IsSuccess = true;
            return response;
        }

        //Delete Url
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
        public async Task<GetByShortCodeResponse> GetByShortCode(string shortCode, string userAgent, string ipAdress)
        {
            var response = new GetByShortCodeResponse();
            var entity = await _originalUrlRepository.GetByShortCode(shortCode);
            if (entity is null)
            {
                response.Error = "URL with this short code could not be found!";
                response.IsSuccess = false;
                return response;
            }
            if (entity.ExpirationDate < DateTime.UtcNow)
            {
                response.Error = "URL with this short code has expired!";
                response.IsSuccess = false;
                return response;
            }
            entity.IncrementClickCount();
            await _originalUrlRepository.Update(entity);
            await _analyticService.LogAnalytic(shortCode, userAgent, ipAdress);
            response.OriginalUrl = entity.OriginalLink;
            response.IsSuccess = true;
            return response;
        }

        //This is method to get all the details
        public async Task<GetUrlDetailsResponse> GetUrlDetails(string shortCode)
        {
            var response = new GetUrlDetailsResponse();
            var originalUrl = await _originalUrlRepository.GetByShortCode(shortCode);
            var analytics = await _analyticRepository.GetByShortCode(shortCode);
            if (originalUrl is null)
            {
                response.IsSuccess = false;
                response.Error = "Can't find record by that short code";
                return response;
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
                response.IsActive = originalUrl.IsActive;
                return response;
            }

        }

        //Update method
        public async Task<UpdateOriginalUrlResponse> UpdateUrl(UpdateOriginalUrlRequest request)
        {
            var response = new UpdateOriginalUrlResponse();
            var entity = await _originalUrlRepository.GetByShortCode(request.ShortCode);

            if (request.OriginalUrl is null && request.ExpirationDate is null && request.IsActive is null)
            {
                response.IsSuccess = false;
                response.Error = "There is nothing to update";
                return response;
            }

            entity.SetExpirationDate(request.ExpirationDate ?? entity.ExpirationDate);
            entity.UpdateOriginalLink(request.OriginalUrl ?? entity.OriginalLink);
            entity.SetIsActive(request.IsActive ?? entity.IsActive);
            await _originalUrlRepository.Update(entity);
            response.IsSuccess = true;
            return response;
        }

        public async Task DeleteExpiredUrls()
        {
            var expiredUrls = await _originalUrlRepository.GetExpiredUrls();
            foreach (var url in expiredUrls)
            {
                await _originalUrlRepository.Deactivate(url.ShortCode);
            }
        }
    }
}
