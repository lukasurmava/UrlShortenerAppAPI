using UrlShortenerApp.Service.Requests;
using UrlShortenerApp.Service.Responses;

namespace UrlShortenerApp.Service.Abstractions
{
    public interface IOriginalUrlService
    {
        public Task<GetByShortCodeResponse> GetByShortCode(string shortCode, string userAgent, string ipAdress);
        public Task<CreateOriginalUrlResponse> CreateShortUrl(CreateOriginalUrlRequest request);
        public Task<GetUrlDetailsResponse> GetUrlDetails(string shortCode);

        public Task<UpdateOriginalUrlResponse> UpdateUrl(UpdateOriginalUrlRequest request);
        public Task<DeleteOriginalUrlResponse> DeleteUrl(string shortCode);
        public Task DeleteExpiredUrls();
    }
}
