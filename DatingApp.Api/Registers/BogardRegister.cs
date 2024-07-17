
using DatingApp.Application.UserProfiles.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.Api.Registers
{
    public class BogardRegister : IWebApplicationBuilderRegistar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Program),typeof(GetAllUserProfiles));
            builder.Services.AddMediatR(typeof(GetAllUserProfiles));
        }
    }
}
