using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortenerApp.Domain
{
    public class OriginalUrl : Entity
    {
        public string OriginalLink { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public int ClickCount { get; set; }
        public bool IsActive { get; set; }
    }
}
