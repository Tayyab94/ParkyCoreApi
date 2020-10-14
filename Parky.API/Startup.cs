using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Parky.API.Data;
using Parky.API.Mapper;
using Parky.API.Repositories.NationalParkRepository;
using Parky.API.Repositories.TrailRepository;
using Parky.API.Repositories.UserRepository;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Parky.API
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

            services.AddCors();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefauleConnection"));
            });

            services.AddScoped<INationalParkRepository, NationalParkRepository>();

            services.AddScoped<ITrailRepository, TrailRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(ParkyMappings));

            services.AddApiVersioning(Options =>
            {
                Options.AssumeDefaultVersionWhenUnspecified = true;
                Options.DefaultApiVersion = new ApiVersion(1, 0);
                Options.ReportApiVersions = true;
            });



            // if we want to move the Api Verioning Configuration in saparage file then ..
            services.AddVersionedApiExplorer(Options => Options.GroupNameFormat = "'v'VVV");

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen();



            //// For Adding the Versioning in Api. need to install the two packeges. (Microsoft.AspNetCore.Mvc.Versioning, Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer)
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("ParkyOpenAPISpec_NP",
            //        new Microsoft.OpenApi.Models.OpenApiInfo()
            //        {
            //            Title="parky API (Nation Park)",
            //            Version="1",
            //            Description= "This is API of Parky Application (Nation Park)", // if you want to add the desciption 
            //            Contact=new Microsoft.OpenApi.Models.OpenApiContact()
            //            {
            //                Email="Parkyapi@gmail.com", 
            //                Name="Quick Developing",
            //                Url=new Uri("https://www.google.com")
            //            },
            //            License=new Microsoft.OpenApi.Models.OpenApiLicense()
            //            {
            //                Name="MIT License",
            //                Url=new Uri("https://en.wikipedia.org/wiki/MIT_License")
            //            }

            //        });

            //    // For the Multiple API Documentation

            //    //options.SwaggerDoc("ParkyOpenAPISpec_Tril",
            //    //new Microsoft.OpenApi.Models.OpenApiInfo()
            //    //{
            //    //    Title = "parky API (Trail)",
            //    //    Version = "1",
            //    //    Description = "This is API of Parky Application (Trail)", // if you want to add the desciption 
            //    //        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            //    //    {
            //    //        Email = "Parkyapi@gmail.com",
            //    //        Name = "Quick Developing",
            //    //        Url = new Uri("https://www.google.com")
            //    //    },
            //    //    License = new Microsoft.OpenApi.Models.OpenApiLicense()
            //    //    {
            //    //        Name = "MIT License",
            //    //        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
            //    //    }

            //    //});


            //    // To get the comment of Api Controller
            //    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var cmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            //    options.IncludeXmlComments(cmlCommentFullPath);
            //    // Till Now.


            //});


            // fOR Configuration the JWT token 
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);


            var appSettings = appSettingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.secret);

            services.AddAuthentication(s =>
            {
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseHttpsRedirection();


        //    app.UseSwagger();  // For Documentation of API

        //    app.UseSwaggerUI(op => {
        //        op.SwaggerEndpoint("/Swagger/ParkyOpenAPISpec_NP/Swagger.json", "Parky API Nation Park");
        //        // For the Multiple API Documentation
        //        //op.SwaggerEndpoint("/Swagger/ParkyOpenAPISpec_Tril/Swagger.json", "Parky API Tails");

        //        op.RoutePrefix = "";  //  set the Default page, Oepn Swaggger UI page but remove the defaule APi route from the property/Launch-Json file
        //    }); // View the Graphicle View Of API in Swagger.

        //    app.UseRouting();

        //    app.UseAuthorization();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });
        //}





        // if we want to move the Api Verioning Configuration in saparage file then ..
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            app.UseSwagger();  // For Documentation of API

            app.UseSwaggerUI(op => {
                foreach (var item in provider.ApiVersionDescriptions)
                {
                    op.SwaggerEndpoint($"/Swagger/{item.GroupName}/Swagger.json", item.GroupName.ToUpperInvariant());
                }
                op.RoutePrefix = "";  //  set the Default page, Oepn Swaggger UI page but remove the defaule APi route from the property/Launch-Json file
            }); // View the Graphicle View Of API in Swagger.


            //app.UseSwaggerUI(op => {
            //    op.SwaggerEndpoint("/Swagger/ParkyOpenAPISpec_NP/Swagger.json", "Parky API Nation Park");
            //    // For the Multiple API Documentation
            //    //op.SwaggerEndpoint("/Swagger/ParkyOpenAPISpec_Tril/Swagger.json", "Parky API Tails");

            //    op.RoutePrefix = "";  //  set the Default page, Oepn Swaggger UI page but remove the defaule APi route from the property/Launch-Json file
            //}); // View the Graphicle View Of API in Swagger.

            app.UseRouting();


            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());


            app.UseAuthentication();   // Authentacation of the User
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
