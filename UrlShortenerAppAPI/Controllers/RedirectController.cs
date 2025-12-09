using Microsoft.AspNetCore.Mvc;
using UrlShortenerApp.Service.Abstractions;

namespace UrlShortenerApp.API.Controllers
{
    [ApiController]
    [Route("")]
    public class RedirectController : ControllerBase
    {
        private readonly IOriginalUrlService _originalUrlService;

        public RedirectController(IOriginalUrlService originalUrlService)
        {
            _originalUrlService = originalUrlService;
        }
        [HttpGet("{shortCode}")]
        public async Task<IActionResult> GetByShortCode(string shortCode)
        {
            var userAgent = Request.Headers["User-Agent"].ToString();
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var response = await _originalUrlService.GetByShortCode(shortCode, userAgent, ipAddress);
            if (!response.IsSuccess)
            {
                return NotFound(response.Error);
            }
            return Redirect(response.OriginalUrl);
        }
    }
}
