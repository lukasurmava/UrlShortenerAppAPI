using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortenerApp.Domain
{
    public abstract class Entity
    {
        [Key]
        public string ShortCode { get; set; }
    }
}
