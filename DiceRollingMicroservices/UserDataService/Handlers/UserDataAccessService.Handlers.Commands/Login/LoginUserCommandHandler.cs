namespace UserDataAccessService.Handlers.Commands.Login
{
    using System.Threading.Tasks;

    using MediatR;
    
    using Microsoft.AspNetCore.Identity;
    
    using UserDataAccessService.Data.Models;
    using UserDataAccessService.Handlers.Commands.Response;
    using UserDataAccessService.Handlers.Commands.Token;
    using UserDataAccessService.Handlers.Queries.Token;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponse>
    {
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;

        public LoginUserCommandHandler(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        public async Task<TokenResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new TokenResponse("Username does not exists");
            }

            bool isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return new TokenResponse("Invalid password");
            }

            RefreshToken refreshToken = await mediator.Send(new GetTokenQuery(user.Id));

            if (refreshToken != null)
            {
                await mediator.Send(new RevokeTokenCommand(refreshToken));
            }

            TokenResponse response = await mediator.Send(new CreateAccessTokenCommand(user.Id, request.Email));
            return response;
        }
    }
}
