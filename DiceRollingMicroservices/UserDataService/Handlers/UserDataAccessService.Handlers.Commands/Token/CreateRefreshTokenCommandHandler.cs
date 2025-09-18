namespace UserDataAccessService.Handlers.Commands.Token
{
    using MediatR;

    using UserDataAccessService.Data.Models;
    using UserDataAccessService.Handlers.Commands.Response;
    using UserDataAccessService.Handlers.Queries.Token;

    public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, TokenResponse>
    {
        private readonly IMediator mediator;

        public CreateRefreshTokenCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<TokenResponse> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            RefreshToken refreshToken = await mediator.Send(new GetTokenQuery() { Token = request.RefreshToken }, cancellationToken);

            if (refreshToken != null)
            {
                await mediator.Send(new RevokeTokenCommand(refreshToken), cancellationToken);
                TokenResponse newToken = await mediator.Send(new CreateAccessTokenCommand(refreshToken.UserId, refreshToken.User.Email), cancellationToken);

                return newToken;
            }

            return new TokenResponse("Refresh token does not exist!");
        }
    }
}
