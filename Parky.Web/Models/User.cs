using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parky.Web.Models
{
    public class User
    {
        public string Password { get; set; }
        public string UserName { get; set; }

        public string Role { get; set; }
        public string Token { get; set; }
    }
}
