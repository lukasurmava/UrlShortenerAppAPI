using UrlShortenerApp.Domain;

namespace UrlShortenerApp.Service.Responses
{
    public record GetUrlDetailsResponse(
        string OriginalUrl,
        string ShortCode,
        DateTime CreatedOn,
        DateTime ExpirationDate,
        bool IsActive,
        int ClickCount,
        List<Analytic> Analytics
        ) : ResponseBase(true, null);
}
