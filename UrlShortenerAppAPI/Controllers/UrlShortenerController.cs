using Microsoft.AspNetCore.Mvc;
using UrlShortenerApp.Service.Abstractions;
using UrlShortenerApp.Service.Requests;
using UrlShortenerApp.Service.Responses;

namespace UrlShortenerApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IOriginalUrlService _originalUrlService;
        public UrlShortenerController(IOriginalUrlService originalUrlService)
        {
            _originalUrlService = originalUrlService;
        }

        [HttpGet]
        public async Task<ActionResult<CreateOriginalUrlResponse>> Create(CreateOriginalUrlRequest createOriginalUrlRequest)
        {
            var response = await _originalUrlService.CreateShortUrl(createOriginalUrlRequest);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }
            return response;
        }
    }
}
