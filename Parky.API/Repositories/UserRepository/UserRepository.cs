using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Parky.API.Data;
using Parky.API.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Parky.API.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings appSettings;

        public UserRepository(ApplicationDbContext context,IOptions<AppSettings> appSettings)
        {
            Context = context;
            this.appSettings = appSettings.Value;
        }

        public ApplicationDbContext Context { get; }

        public User Authenticate(string userName, string password)
        {
            var user = Context.Users.SingleOrDefault(s => s.Password.Equals(password) && s.UserName.Equals(userName));

            if(user==null)
            {
                return null; 
            }


            // if the user found, Generate the JTW Token

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                 new Claim(ClaimTypes.Name,user.Id.ToString()),
                  new Claim(ClaimTypes.Role,user.Role.ToString())
                }),

                Expires = DateTime.UtcNow.AddDays(7),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public bool IsUniqueUser(string UserName)
        {
            var user = Context.Users.SingleOrDefault(s => s.UserName == UserName);

            if (user == null)
                return true;
            return false;
        }

        public User Register(string UserName, string Password)
        {
            var user = new User()
            {
                UserName = UserName,
                Password = Password,
                Role="Admin"
            };

            Context.Users.Add(user);

            Context.SaveChanges();

            return user;
        }
    }
}
