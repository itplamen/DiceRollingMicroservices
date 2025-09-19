namespace UserDataAccessService.Infrastructure.IoC.Packages
{
    using MediatR;
    
    using Microsoft.Extensions.DependencyInjection;

    using DiceRollingMicroservices.Common.Models.Response;
    using UserDataAccessService.Data.Models;
    using UserDataAccessService.Handlers.Commands.Login;
    using UserDataAccessService.Handlers.Commands.Logout;
    using UserDataAccessService.Handlers.Commands.Register;
    using UserDataAccessService.Handlers.Commands.Response;
    using UserDataAccessService.Handlers.Commands.Token;
    using UserDataAccessService.Handlers.Queries.Token;
    using UserDataAccessService.Infrastructure.IoC.Contracts;

    public sealed class HandlersPackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetTokenQuery, RefreshToken>, GetTokenQueryHandler>();
            services.AddScoped<IRequestHandler<CreateAccessTokenCommand, TokenResponse>, CreateAccessTokenCommandHandler>();
            services.AddScoped<IRequestHandler<RevokeTokenCommand>, RevokeTokenCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterUserCommand, BaseResponse>, RegisterUserCommandHandler>();
            services.AddScoped<IRequestHandler<LoginUserCommand, TokenResponse>, LoginUserCommandHandler>();
            services.AddScoped<IRequestHandler<CreateRefreshTokenCommand, TokenResponse>, CreateRefreshTokenCommandHandler>();
            services.AddScoped<IRequestHandler<LogoutUserCommand, BaseResponse>, LogoutUserCommandHandler>();
        }
    }
}
