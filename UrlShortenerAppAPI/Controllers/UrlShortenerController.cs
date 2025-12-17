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
        private readonly IAnalyticService _analyticService;
        public UrlShortenerController(IOriginalUrlService originalUrlService, IAnalyticService analyticService)
        {
            _originalUrlService = originalUrlService;
            _analyticService = analyticService;
        }

        //Create Short URL
        [HttpPost]
        public async Task<ActionResult<ResponseBase>> Create(CreateOriginalUrlRequest createOriginalUrlRequest)
        {
            var response = await _originalUrlService.CreateShortUrl(createOriginalUrlRequest);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }
            return response;
        }

        //Delete Short URL
        [HttpGet("details/{shortCode}")]
        public async Task<ActionResult<ResponseBase>> GetUrlDetails(string shortCode)
        {
            var response = await _originalUrlService.GetUrlDetails(shortCode);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }
            return response;
        }

        //Update Short URL
        [HttpPut]
        public async Task<ActionResult<ResponseBase>> Update(UpdateOriginalUrlRequest request)
        {
            var response = await _originalUrlService.UpdateUrl(request);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }
            return response;
        }

        //Delete Short URL
        [HttpDelete("{shortCode}")]
        public async Task<ActionResult<ResponseBase>> Delete([FromRoute] string shortCode)
        {
            var response = await _originalUrlService.DeleteUrl(shortCode);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }
            return response;
        }
    }
}
