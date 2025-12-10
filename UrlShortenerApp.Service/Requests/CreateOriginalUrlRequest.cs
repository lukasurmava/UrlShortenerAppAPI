using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortenerApp.Service.Requests
{
    public class CreateOriginalUrlRequest
    {
        public string OriginalUrl { get; set; }
        public DateTime ExpireDate { get; set; }
        public string? Alias { get; set; }
    }
}
