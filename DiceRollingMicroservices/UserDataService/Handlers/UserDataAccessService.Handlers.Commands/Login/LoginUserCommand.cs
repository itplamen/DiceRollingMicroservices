namespace UserDataAccessService.Handlers.Commands.Login
{
    using MediatR;

    using UserDataAccessService.Handlers.Commands.Response;

    public class LoginUserCommand : IRequest<TokenResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
