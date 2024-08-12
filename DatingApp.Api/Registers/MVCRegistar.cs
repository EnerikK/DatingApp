﻿using DatingApp.Api.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace DatingApp.Api.Registers
{
    public class MVCRegistar : IWebApplicationBuilderRegistar
    {
 
        public void RegisterServices(WebApplicationBuilder builder)
        {

            // Add services to the container.

            builder.Services.AddControllers();
               
            builder.Services.AddApiVersioning(
                config =>
                {
                    config.DefaultApiVersion = new ApiVersion(1, 0);
                    config.AssumeDefaultVersionWhenUnspecified = true;
                    //When Clients make calls when they dont specify
                    //the version we dont want to return erros 
                    //or null so lets use this to assume the correct version
                    config.ReportApiVersions = true; //To each response adds a header which tells what are the supported APIVersion
                    config.ApiVersionReader = new UrlSegmentApiVersionReader(); //Instructs the api versioning what aproach about the version we go for
                });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddVersionedApiExplorer(config =>
            {
                config.GroupNameFormat = "'v'VVV";
                config.SubstituteApiVersionInUrl = true;
            });
            
            builder.Services.AddEndpointsApiExplorer();

        }
    }
}
