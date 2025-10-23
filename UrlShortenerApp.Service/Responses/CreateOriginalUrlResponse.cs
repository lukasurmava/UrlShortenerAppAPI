using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
