namespace UrlShortenerApp.Service.Responses
{
    public record FailedResponse(string Error) : ResponseBase(false, Error);
}
