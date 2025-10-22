using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortenerApp.Domain
{
    public class Analytic : Entity
    {
        public DateTime ClickDate { get; set; }
        public string UserAgent { get; set; }
        public string IpAdress { get; set; }
    }
}
