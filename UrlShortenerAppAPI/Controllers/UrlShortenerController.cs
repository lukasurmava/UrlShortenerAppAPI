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

        [HttpGet]
        public async Task<ActionResult<GetByShortCodeResponse>> GetByShortCode(string shortCode)
        {
            var response = await _originalUrlService.GetByShortCode(shortCode);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }
            return response;
        }

        [HttpGet]
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

        [HttpDelete]
        public async Task<ActionResult<DeleteOriginalUrlResponse>> Delete(string shortCode)
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
