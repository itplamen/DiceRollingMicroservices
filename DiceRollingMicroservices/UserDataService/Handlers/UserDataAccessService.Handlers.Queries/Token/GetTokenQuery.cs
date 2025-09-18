namespace UserDataAccessService.Handlers.Queries.Token
{
    using MediatR;

    using UserDataAccessService.Data.Models;

    public class GetTokenQuery : IRequest<RefreshToken>
    {
        public string Token { get; set; }
    }
}
