using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parky.Web.Models;
using Parky.Web.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parky.Web.Repository
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
        private readonly IHttpClientFactory httpClientFactory;

        public AccountRepository(IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<User> LoginAsync(string Url, User objToCreate)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, Url);
            if (objToCreate != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objToCreate),
                    Encoding.UTF8, "application/json");
            }
            else
            {
                return new User();
            }

            var client = httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.SendAsync(request);
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                //var jsonString = responseMessage.Content.ReadAsStringAsync();
                //return JsonConvert.DeserializeObject<User>(jsonString);
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(jsonString);
            }
            else
            {
                return new User();
            }

        }

        public async Task<bool> RegisterUserAsync(string Url, User objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Url);

            if (objToCreate != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
