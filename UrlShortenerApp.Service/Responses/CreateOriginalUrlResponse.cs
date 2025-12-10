namespace UrlShortenerApp.Service.Responses
{
    public class CreateOriginalUrlResponse : ResponseBase
    {
        public string ShortUrl { get; set; }
        public string OriginalUrl { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int ClickCount { get; set; }
    }
}
