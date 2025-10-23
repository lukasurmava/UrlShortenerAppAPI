using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortenerApp.Service.Responses
{
    public abstract class ResponseBase
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
    }
}
