using UrlShortenerApp.Service.Requests;
using UrlShortenerApp.Service.Responses;

namespace UrlShortenerApp.Service.Abstractions
{
    public interface IOriginalUrlService
    {
        public Task<ResponseBase> GetByShortCode(string shortCode, string userAgent, string ipAdress);
        public Task<ResponseBase> CreateShortUrl(CreateOriginalUrlRequest request);
        public Task<ResponseBase> GetUrlDetails(string shortCode);

        public Task<ResponseBase> UpdateUrl(UpdateOriginalUrlRequest request);
        public Task<ResponseBase> DeleteUrl(string shortCode);
        public Task DeleteExpiredUrls();
    }
}
