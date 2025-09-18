namespace UserDataAccessService.Handlers.Commands.Token
{
    using MediatR;

    using UserDataAccessService.Data.Models;

    public class RevokeTokenCommand : IRequest
    {
        public RefreshToken RefreshToken { get; set; }
    }
}
