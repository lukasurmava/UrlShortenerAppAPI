namespace UrlShortenerApp.Service.Requests
{
    public record UpdateOriginalUrlRequest(
        string ShortCode,
        string? OriginalUrl,
        DateTime? ExpirationDate,
        bool? IsActive
        );
}
