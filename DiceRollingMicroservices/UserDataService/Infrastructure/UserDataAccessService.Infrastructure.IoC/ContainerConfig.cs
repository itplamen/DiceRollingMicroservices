namespace UserDataAccessService.Infrastructure.IoC
{
    using Microsoft.Extensions.DependencyInjection;

    using DiceRollingMicroservices.Common.Models.IoC;
    using UserDataAccessService.Infrastructure.IoC.Packages;
    using Microsoft.Extensions.Configuration;

    public static class ContainerConfig
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            IPackage[] packages =
            {
                new ApiPackage(),
                new DataPackage(),
                new HandlersPackage(),
                new MessageBusPackage(configuration)
            };

            RegisterServices(services, packages);
        }

        private static void RegisterServices(IServiceCollection services, IPackage[] packages)
        {
            foreach (var package in packages)
            {
                package.RegisterServices(services);
            }
        }
    }
}
