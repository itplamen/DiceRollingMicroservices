namespace UserDataAccessService.Infrastructure.IoC.Contracts
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IPackage
    {
        void RegisterServices(IServiceCollection services);
    }
}
