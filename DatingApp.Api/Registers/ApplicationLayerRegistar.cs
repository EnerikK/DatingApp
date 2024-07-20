﻿using DatingApp.Application.Services;

namespace DatingApp.Api.Registers;

public class ApplicationLayerRegistar : IWebApplicationBuilderRegistar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IdentityService>();
    }
}