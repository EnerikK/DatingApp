namespace DatingApp.Api.Registers
{
    public interface IWebApplicationRegistar : IRegistar
    {
        public void RegisterPipelineComponents(WebApplication app);
    }
}
