using Parky.Web.Models;
using Parky.Web.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parky.Web.Repository
{
    public class NationalParkRepository:Repository<NationalPark>,INationalParkRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NationalParkRepository(IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
    }
}
