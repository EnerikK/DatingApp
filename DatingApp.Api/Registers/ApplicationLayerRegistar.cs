using System.Text.Json;
using DatingApp.Api.Interfaces;
using DatingApp.Api.Options;
using DatingApp.Application.Services;

namespace DatingApp.Api.Registers;

public class ApplicationLayerRegistar : IWebApplicationBuilderRegistar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IdentityService>();
        builder.Services.AddScoped<IPhotoService, PhotoService>();
        builder.Services.AddScoped<IMessageRepository, MessageRepository>();
    }
}