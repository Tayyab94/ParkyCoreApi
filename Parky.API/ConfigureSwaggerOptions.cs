using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;

namespace Parky.API
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var item in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    item.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title=$"Parky APi {item.ApiVersion}",
                        Version=item.ApiVersion.ToString()
                    });
            }



            // To Implement the jwtBearer in swagger..
            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Description="Welcome to the Swagger Bearer ScreenView",
                Name ="Authorization",
                 In=Microsoft.OpenApi.Models.ParameterLocation.Header,
                 Type=Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                 Scheme="Bearer"
            });


            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
            {
                {
                     new OpenApiSecurityScheme
                    {
                         Reference=new OpenApiReference
                         {
                             Type=ReferenceType.SecurityScheme,
                              Id="Bearer"
                         },
                         Scheme="oauth2",
                         Name="Bearer",
                         In=ParameterLocation.Header,
                    },
                    new List<string>()
                }
                   

            });


            // To get the comment of Api Controller
            var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var cmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            options.IncludeXmlComments(cmlCommentFullPath);
            // Till Now.
        }
    }
}
