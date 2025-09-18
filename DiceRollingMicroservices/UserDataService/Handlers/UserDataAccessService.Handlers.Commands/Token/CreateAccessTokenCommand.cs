namespace UserDataAccessService.Handlers.Commands.Token
{
    using MediatR;

    using UserDataAccessService.Handlers.Commands.Response;

    public class CreateAccessTokenCommand : IRequest<TokenResponse>
    {
        public CreateAccessTokenCommand(int userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        public int UserId { get; set; }

        public string Email { get; set; }
    }
}
