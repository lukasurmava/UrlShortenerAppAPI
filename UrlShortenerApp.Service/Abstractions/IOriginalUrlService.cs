using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortenerApp.Service.Requests;
using UrlShortenerApp.Service.Responses;

namespace UrlShortenerApp.Service.Abstractions
{
    public interface IOriginalUrlService
    {
        public Task GetByShortCode(string shortCode);
        public Task<CreateOriginalUrlResponse> CreateShortUrl(CreateOriginalUrlRequest request);
        public Task<GetUrlDetailsResponse> GetUrlDetails(string shortCode);

        public Task<UpdateOriginalUrlResponse> UpdateUrl(UpdateOriginalUrlRequest request);
        public Task<DeleteOriginalUrlResponse> DeleteUrl(string shortCode);
    }
}
