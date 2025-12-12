namespace UrlShortenerApp.Service.Responses
{
    public record CreateOriginalUrlResponse(
        string ShortUrl,
        string OriginalUrl,
        DateTime ExpirationDate,
        int ClickCount,
        bool IsSuccess,
        string? Error
        ) : ResponseBase(IsSuccess, Error);
}
