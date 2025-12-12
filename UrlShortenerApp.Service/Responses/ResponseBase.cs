namespace UrlShortenerApp.Service.Responses
{
    public record ResponseBase
    (
        bool IsSuccess,
        string? Error
    );
}
