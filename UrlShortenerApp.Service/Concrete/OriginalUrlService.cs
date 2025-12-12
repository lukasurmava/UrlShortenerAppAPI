using UrlShortenerApp.Infrastructure.Abstractions;
using UrlShortenerApp.Infrastructure;
using UrlShortenerApp.Service.Abstractions;
using UrlShortenerApp.Service.Requests;
using UrlShortenerApp.Service.Responses;
using UrlShortenerApp.Domain;

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
        public async Task<ResponseBase> CreateShortUrl(CreateOriginalUrlRequest request)
        {
            string shortcode;

            if (request.ExpireDate < DateTime.UtcNow)
            {
                var response = new FailedResponse("Expiredate can't be less then current date");

                return response;
            }
            if (!string.IsNullOrEmpty(request.Alias))
            {
                var existingUrl = await _originalUrlRepository.ShortCodeExist(request.Alias);

                if (existingUrl)
                {
                    var response = new FailedResponse("Alias is already in use.");

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
                var response = new FailedResponse("The provided URL is not valid.");

                return response;
            }
            else
            {
                var originalUrl = new OriginalUrl(shortcode, request.OriginalUrl, request.ExpireDate);

                await _originalUrlRepository.Create(originalUrl);

                var response = new CreateOriginalUrlResponse(
                    originalUrl.ShortCode,
                    originalUrl.OriginalLink,
                    originalUrl.ExpirationDate,
                    originalUrl.ClickCount
                    );

                return response;
            }


        }

        //Delete Url
        public async Task<ResponseBase> DeleteUrl(string shortCode)
        {
            if (await _originalUrlRepository.GetByShortCode(shortCode) is not null)
            {
                await _originalUrlRepository.Delete(shortCode);
                var response = new DeleteOriginalUrlResponse();
                return response;
            }
            else
            {
                var response = new FailedResponse("Can't find record by this short code");
                return response;
            }
            
        }

        //This is redirect method
        public async Task<ResponseBase> GetByShortCode(string shortCode, string userAgent, string ipAdress)
        {
            var entity = await _originalUrlRepository.GetByShortCode(shortCode);
            if (entity is null)
            {
                var response = new FailedResponse("URL with this short code could not be found!");
                return response;
            }
            if (entity.ExpirationDate < DateTime.UtcNow)
            {
                var response = new FailedResponse("URL with this short code has expired!");
                return response;
            }
            else
            {
                entity.IncrementClickCount();
                await _originalUrlRepository.Update(entity);
                await _analyticService.LogAnalytic(shortCode, userAgent, ipAdress);
                return new GetByShortCodeResponse(entity.OriginalLink);
            }

        }

        //This is method to get all the details
        public async Task<ResponseBase> GetUrlDetails(string shortCode)
        {
            var originalUrl = await _originalUrlRepository.GetByShortCode(shortCode);
            var analytics = await _analyticRepository.GetByShortCode(shortCode);
            if (originalUrl is null)
            {
                var response = new FailedResponse("Can't find record by that short code");
                return response;
            }
            else
            {
                return new GetUrlDetailsResponse(
                    originalUrl.OriginalLink,
                    originalUrl.ShortCode,
                    originalUrl.CreatedOn,
                    originalUrl.ExpirationDate,
                    originalUrl.IsActive,
                    originalUrl.ClickCount,
                    analytics.ToList()
                    );
            }

        }

        //Update method
        public async Task<ResponseBase> UpdateUrl(UpdateOriginalUrlRequest request)
        {
            var entity = await _originalUrlRepository.GetByShortCode(request.ShortCode);

            if (request.OriginalUrl is null && request.ExpirationDate is null && request.IsActive is null)
            {
                return new FailedResponse("There is nothing to update");
            }

            entity.SetExpirationDate(request.ExpirationDate ?? entity.ExpirationDate);
            entity.UpdateOriginalLink(request.OriginalUrl ?? entity.OriginalLink);
            entity.SetIsActive(request.IsActive ?? entity.IsActive);
            await _originalUrlRepository.Update(entity);
            return new UpdateOriginalUrlResponse();
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
