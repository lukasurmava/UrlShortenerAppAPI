namespace UrlShortenerApp.Service.Requests
{

    public record CreateOriginalUrlRequest(
        string OriginalUrl, 
        DateTime ExpireDate, 
        string? Alias
        );

}
