namespace UrlShortenerApp.Service.Requests
{
    public class CreateOriginalUrlRequest
    {
        public string OriginalUrl { get; set; }
        public DateTime ExpireDate { get; set; }
        public string? Alias { get; set; }
    }
}
