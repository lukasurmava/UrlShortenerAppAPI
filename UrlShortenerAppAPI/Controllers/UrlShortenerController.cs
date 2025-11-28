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

        [HttpPost]
        public async Task<ActionResult<CreateOriginalUrlResponse>> Create(CreateOriginalUrlRequest createOriginalUrlRequest)
        {
            var response = await _originalUrlService.CreateShortUrl(createOriginalUrlRequest);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }
            return response;
        }

        [HttpGet("{shortCode}")]
        public async Task<ActionResult<GetByShortCodeResponse>> GetByShortCode([FromRoute] string shortCode)
        {
            var ipAdress = Request.Headers["User-Agent"].ToString();
            var userAgent = HttpContext.Connection.RemoteIpAddress?.ToString();
            var response = await _originalUrlService.GetByShortCode(shortCode, userAgent, ipAdress);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }
            return Redirect(response.OriginalUrl);
        }

        [HttpGet("details/{shortCode}")]
        public async Task<ActionResult<GetUrlDetailsResponse>> GetUrlDetails(string shortCode)
        {
            var response = await _originalUrlService.GetUrlDetails(shortCode);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }
            return response;
        }

        [HttpPut]
        public async Task<ActionResult<UpdateOriginalUrlResponse>> Update(UpdateOriginalUrlRequest request)
        {
            var response = await _originalUrlService.UpdateUrl(request);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }
            return response;
        }

        [HttpDelete("{shortCode}")]
        public async Task<ActionResult<DeleteOriginalUrlResponse>> Delete([FromRoute] string shortCode)
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
