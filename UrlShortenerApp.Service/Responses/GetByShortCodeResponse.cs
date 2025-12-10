namespace UrlShortenerApp.Service.Responses
{
    public class GetByShortCodeResponse : ResponseBase
    {
        public string OriginalUrl { get; set; }
    }
}
