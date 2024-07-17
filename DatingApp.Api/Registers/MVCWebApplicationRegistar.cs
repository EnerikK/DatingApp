using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace DatingApp.Api.Registers
{
    public class MVCWebApplicationRegistar : IWebApplicationRegistar
    {
        public void RegisterPipelineComponents(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>(); //resolves the service

                foreach (var description in provider.ApiVersionDescriptions)
                {// for each var creates a swagger End Point
                    options.SwaggerEndpoint($"/Swagger/{description.GroupName}/Swagger.json",
                    description.ApiVersion.ToString());//string interpolation 
                }

            });

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
                .WithOrigins("http://localhost:4200","https://localhost:4200"));
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
