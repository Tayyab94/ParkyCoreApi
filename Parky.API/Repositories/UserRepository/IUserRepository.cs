using Parky.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parky.API.Repositories.UserRepository
{
    public interface IUserRepository
    {

        bool IsUniqueUser(string UserName);

        User Authenticate(string userName, string password);

        User Register(string UserName, string Password);
    }
}
