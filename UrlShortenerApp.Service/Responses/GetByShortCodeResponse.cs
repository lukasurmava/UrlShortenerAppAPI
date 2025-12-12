namespace UrlShortenerApp.Service.Responses
{
    public record GetByShortCodeResponse(string OriginalUrl) : ResponseBase(true, null);
}
