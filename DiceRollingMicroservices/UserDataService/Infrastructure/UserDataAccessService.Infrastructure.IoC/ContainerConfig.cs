namespace UserDataAccessService.Infrastructure.IoC
{
    using Microsoft.Extensions.DependencyInjection;

    using DiceRollingMicroservices.Common.Models.IoC;
    using UserDataAccessService.Infrastructure.IoC.Packages;

    public static class ContainerConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            IPackage[] packages =
            {
                new ApiPackage(),
                new DataPackage(),
                new HandlersPackage(),
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
