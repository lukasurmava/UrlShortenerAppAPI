namespace UrlShortenerApp.Service.Responses
{
    public record UpdateOriginalUrlResponse(
        bool IsSuccess,
        string? Error
        ) : ResponseBase(IsSuccess, Error);
}
