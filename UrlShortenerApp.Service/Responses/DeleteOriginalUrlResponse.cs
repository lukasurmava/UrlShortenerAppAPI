namespace UrlShortenerApp.Service.Responses
{
    public record DeleteOriginalUrlResponse(
        bool IsSuccess,
        string? Error
        ) : ResponseBase(IsSuccess, Error);
}
