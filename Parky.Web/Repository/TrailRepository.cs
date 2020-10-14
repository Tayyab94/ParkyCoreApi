using Parky.Web.Models;
using Parky.Web.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parky.Web.Repository
{
    public class TrailRepository:Repository<Trail>,ITrailRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TrailRepository(IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;

        }
    }
}
