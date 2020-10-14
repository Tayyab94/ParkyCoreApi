using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parky.Web.Repository.IRepository
{
    public interface IRepository<T> where T:class
    {
        Task<T> GetAsync(string Url, int id,string token);

        Task<IEnumerable<T>> GetAllAsync(string Url, string token);

        Task<bool> CreateAsync(string Url, T ObjToCreate, string token);
        Task<bool> UpdateAsync(string Url, T ObjToUpdagte, string token);
        Task<bool> DeleteAsync(string Url, int id, string token);
    }
}
