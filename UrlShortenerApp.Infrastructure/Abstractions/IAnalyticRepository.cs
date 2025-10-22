using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortenerApp.Domain;

namespace UrlShortenerApp.Infrastructure.Abstractions
{
    public interface IAnalyticRepository
    {
        public Task Create(Analytic analytic);

        public Task<Analytic> GetByShortCode(string shortCode);
        public Task Update(Analytic analytic);
        public Task Delete(string shortCode);
    }
}
