using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Parky.Web.Repository;
using Parky.Web.Repository.IRepository;

namespace Parky.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(op =>
                {
                    op.Cookie.HttpOnly = true;
                    op.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    op.LoginPath = "/Home/Login";
                    op.AccessDeniedPath = "/Home/AccessDenied";
                    op.SlidingExpiration = true;
                });

            services.AddScoped<INationalParkRepository, NationalParkRepository>();
            services.AddScoped<ITrailRepository, TrailRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();  //(We have to add Microsoft.AspnetCore.Mvc.Razor.Runtimecompilation Package) by using this package.. when we do change on view.. at that time we dont need to stop the application.
            services.AddHttpClient(); // When we need to make Http calls to the API




            // Working on session to store the user data..

            services.AddSession(options =>
            {
                // set short timeout for easy testing..
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;

                // Make the session Cookies is essential
                options.Cookie.IsEssential = true;

            });
        
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
