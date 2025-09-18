namespace UserDataAccessService.Handlers.Commands.Token
{
    using MediatR;

    using UserDataAccessService.Handlers.Commands.Response;

    public class CreateTokenCommand : IRequest<TokenResponse>
    {
        public int UserId { get; set; }

        public string Email { get; set; }
    }
}
