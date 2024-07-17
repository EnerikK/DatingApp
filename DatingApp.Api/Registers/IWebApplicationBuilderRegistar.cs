namespace DatingApp.Api.Registers
{
    public interface IWebApplicationBuilderRegistar : IRegistar
    {
        void RegisterServices(WebApplicationBuilder builder);
    }
}
