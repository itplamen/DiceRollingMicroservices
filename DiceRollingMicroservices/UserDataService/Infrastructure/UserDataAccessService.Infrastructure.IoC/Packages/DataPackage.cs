namespace UserDataAccessService.Infrastructure.IoC.Packages
{
    using Microsoft.Extensions.DependencyInjection;

    using DiceRollingMicroservices.Common.Models.IoC;
    using UserDataAccessService.Data;
    using UserDataAccessService.Data.Contracts;
    
    public sealed class DataPackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
        }
    }
}
