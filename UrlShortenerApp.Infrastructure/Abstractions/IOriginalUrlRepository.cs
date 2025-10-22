using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortenerApp.Domain;

namespace UrlShortenerApp.Infrastructure.Abstractions
{
    public interface IOriginalUrlRepository
    {
        public Task Create(OriginalUrl originalUrl);
        public Task<OriginalUrl> GetByShortCode(string shortCode);
        public Task Update(OriginalUrl originalUrl);
        public Task Delete(string shortCode);
    }
}
