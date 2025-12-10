using System.ComponentModel.DataAnnotations;

namespace UrlShortenerApp.Domain
{
    public class Analytic
    {
        private Analytic() { }
        public Analytic(string shortCode, string userAgent, string ipAdress)
        {
            if(string.IsNullOrWhiteSpace(shortCode))
            {
                throw new ArgumentException("Short code cannot be null or empty.", nameof(shortCode));
            }
            if(string.IsNullOrWhiteSpace(userAgent))
            {
                throw new ArgumentException("User agent cannot be null or empty.", nameof(userAgent));
            }
            if(string.IsNullOrWhiteSpace(ipAdress))
            {
                throw new ArgumentException("IP address cannot be null or empty.", nameof(ipAdress));
            }
            ShortCode = shortCode;
            ClickDate = DateTime.UtcNow;
            UserAgent = userAgent;
            IpAdress = ipAdress;
        }
        public int Id { get; private set; }
        public string ShortCode { get; private set; }
        public DateTime ClickDate { get; private set; }
        public string UserAgent { get; private set; }
        public string IpAdress { get; private set; }
    }
}
