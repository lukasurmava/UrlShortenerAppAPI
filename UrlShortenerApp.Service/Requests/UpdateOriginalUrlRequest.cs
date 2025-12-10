namespace UrlShortenerApp.Service.Requests
{
    public class UpdateOriginalUrlRequest
    {
        public string ShortCode { get; set; }
        public string? OriginalUrl { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
