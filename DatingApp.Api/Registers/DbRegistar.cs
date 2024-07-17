using DatingApp.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace DatingApp.Api.Registers
{
    public class DbRegistar : IWebApplicationBuilderRegistar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
          
            builder.Services.AddIdentityCore<IdentityUser>(Options =>
                {
                    Options.Password.RequireDigit = false;
                    Options.Password.RequiredLength = 5;
                    Options.Password.RequireLowercase = false;
                    Options.Password.RequireUppercase = false;
                    Options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<DataContext>();
        }
    }
}
