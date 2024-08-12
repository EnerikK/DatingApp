using DatingApp.Api.Options;
using DatingApp.Api.Registers;

namespace DatingApp.Api.Extensions
{
    public static class RegistarExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
        {   //reflection
            var registrars = GetRegistrars<IWebApplicationBuilderRegistar>(scanningType);

            foreach (var registrar in registrars)
            {
                registrar.RegisterServices(builder);
            }
        }
        public static void RegisterPipelineComponents(this WebApplication app, Type scanningType)
        {
            var registrars = GetRegistrars<IWebApplicationRegistar>(scanningType);
            foreach (var registrar in registrars)
            {
                registrar.RegisterPipelineComponents(app);
            }
        }
        private static IEnumerable<T> GetRegistrars<T>(Type scanningType) where T : IRegistar
        {
            return scanningType.Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<T>();
        }
    }
}

