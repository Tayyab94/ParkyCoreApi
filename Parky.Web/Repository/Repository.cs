using Newtonsoft.Json;
using Parky.Web.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Parky.Web.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public Repository(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateAsync(string Url, T ObjToCreate, string token="")
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Url);
            if(ObjToCreate!=null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(ObjToCreate),
                    Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _httpClientFactory.CreateClient();
            if(token!=null && token.Length!=0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage responseMessage = await client.SendAsync(request);
            if(responseMessage.StatusCode==HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
         
        }

        public async Task<bool> DeleteAsync(string Url, int id, string token = "")
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, Url + id);

            var client = _httpClientFactory.CreateClient();


            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage responseMessage = await client.SendAsync(request);
            if(responseMessage.StatusCode==HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }

        public  async Task<IEnumerable<T>> GetAllAsync(string Url, string token = "")
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Url);

            var client = _httpClientFactory.CreateClient();


            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage responseMessage = await client.SendAsync(request);

            if(responseMessage.StatusCode==HttpStatusCode.OK)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }

            return null;
        }

        public  async Task<T> GetAsync(string Url, int id, string token = "")
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Url+id);

            var client = _httpClientFactory.CreateClient();


            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage responseMessage = await client.SendAsync(request);

            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(jsonString);
            }

            return null;
        }
            

        public async Task<bool> UpdateAsync(string Url, T ObjToUpdagte, string token = "")
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, Url);
            if (ObjToUpdagte != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(ObjToUpdagte),
                    Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _httpClientFactory.CreateClient();

            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage responseMessage = await client.SendAsync(request);
            if (responseMessage.StatusCode == HttpStatusCode.NoContent)
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
