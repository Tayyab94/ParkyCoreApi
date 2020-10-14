using Parky.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parky.Web.Repository.IRepository
{
    public interface IAccountRepository:IRepository<User>
    {

        Task<User> LoginAsync(string Url, User objToCreate);
        Task<bool> RegisterUserAsync(string Url, User objToCreate);
    }
}
