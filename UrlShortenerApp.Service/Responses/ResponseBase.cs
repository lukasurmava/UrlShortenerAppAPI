namespace UrlShortenerApp.Service.Responses
{
    public abstract class ResponseBase
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
    }
}
