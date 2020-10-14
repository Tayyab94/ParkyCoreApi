using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parky.Web.Models;
using Parky.Web.Models.ViewModels;
using Parky.Web.Repository.IRepository;

namespace Parky.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INationalParkRepository nationalParkRepository;
        private readonly ITrailRepository trailRepository;
        private readonly IAccountRepository accountRepository;

        public HomeController(ILogger<HomeController> logger,
            INationalParkRepository nationalParkRepository,
            ITrailRepository trailRepository, IAccountRepository accountRepository)
        {
            _logger = logger;
            this.nationalParkRepository = nationalParkRepository;
            this.trailRepository = trailRepository;
            this.accountRepository = accountRepository;
        }

        public async Task<IActionResult> Index()
        {
            IndexVM obj = new IndexVM()
            {
                NationalParksList = await nationalParkRepository.GetAllAsync(SD.NationalParkAPIUrl, HttpContext.Session.GetString("JWTToken")),
                Trails = await trailRepository.GetAllAsync(SD.TrainAPIUrl, HttpContext.Session.GetString("JWTToken"))
            };
            return View(obj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public IActionResult Login()
        {
            User obj = new User();
            return View(obj);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User obj)
        {
            var objUser = await accountRepository.LoginAsync(SD.UserAccountAPIUrl + "authenticate/", obj);

            if(objUser.Token==null)
            {
                return View();
            }


            // Add Cookie 
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, objUser.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, objUser.Role));
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("JWTToken", objUser.Token);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Register()
        {
          
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User obj)
        {
            bool result = await accountRepository.RegisterUserAsync(SD.UserAccountAPIUrl + "register/", obj);
            if (!result)
                return View();

            return RedirectToAction("login");
        }

       public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWTToken", "");

            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {

            return View();
        }

    }
}
