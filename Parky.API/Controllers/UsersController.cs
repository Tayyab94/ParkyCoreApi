using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parky.API.Models;
using Parky.API.Models.DTOs;
using Parky.API.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parky.API.Controllers
{
   
 
    [Route("api/v{version:apiVersion}/Users")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class UsersController:ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserViewModel model)
        {
            var user = userRepository.Authenticate(model.UserName, model.UserPassword);

            if(user==null)
            {
                return BadRequest(new { message = "User Name or Password is Incorrect" });
            }


            return Ok(user);

        }



        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserViewModel model)
        {
            if(userRepository.IsUniqueUser(model.UserName)==false)
            {
                return BadRequest(new { message = "User is alredy exist" });
            }

            var user = userRepository.Register(model.UserName, model.UserPassword);

            if(user==null)
                return BadRequest(new { message = "Something is wrong, Pleae try again" });

            return Ok();

        }
    }
}
