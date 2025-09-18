namespace UserDataAccessService.Handlers.Commands.Token
{
    using MediatR;

    using UserDataAccessService.Handlers.Commands.Response;

    public class CreateRefreshTokenCommand : IRequest<TokenResponse>
    {
        public string RefreshToken { get; set; }
    }
}
