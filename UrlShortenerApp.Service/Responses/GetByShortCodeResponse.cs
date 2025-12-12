namespace UrlShortenerApp.Service.Responses
{
    public record GetByShortCodeResponse(
        string OriginalUrl,
        bool IsSuccess,
        string? Error
        ) : ResponseBase(IsSuccess, Error);
}
