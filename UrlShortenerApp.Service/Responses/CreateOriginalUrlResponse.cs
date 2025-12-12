namespace UrlShortenerApp.Service.Responses
{
    public record CreateOriginalUrlResponse(
        string ShortUrl,
        string OriginalUrl,
        DateTime ExpirationDate,
        int ClickCount
        ) : ResponseBase(true, null);
}
