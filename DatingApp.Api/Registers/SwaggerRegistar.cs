using DatingApp.Api.Options;

namespace DatingApp.Api.Registers
{
    public class SwaggerRegistar : IWebApplicationBuilderRegistar
    {

        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
        }
    }
}
