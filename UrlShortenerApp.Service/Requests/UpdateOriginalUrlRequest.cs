using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
