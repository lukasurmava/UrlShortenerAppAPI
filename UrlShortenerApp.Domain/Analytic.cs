using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortenerApp.Domain
{
    public class Analytic
    {
        [Key]
        public int Id { get; set; }
        public string ShortCode { get; set; }
        public DateTime ClickDate { get; set; }
        public string UserAgent { get; set; }
        public string IpAdress { get; set; }
    }
}
