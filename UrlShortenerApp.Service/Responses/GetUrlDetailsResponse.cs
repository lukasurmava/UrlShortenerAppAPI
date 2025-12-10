using UrlShortenerApp.Domain;

namespace UrlShortenerApp.Service.Responses
{
    public class GetUrlDetailsResponse : ResponseBase
    {
        public string OriginalUrl { get; set; }
        public string ShortCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int ClickCount { get; set; }
        public List<Analytic> Analytics { get; set; }
    }
}
