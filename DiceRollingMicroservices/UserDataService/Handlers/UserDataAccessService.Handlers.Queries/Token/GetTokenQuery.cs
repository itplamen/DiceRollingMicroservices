namespace UserDataAccessService.Handlers.Queries.Token
{
    using MediatR;

    using UserDataAccessService.Data.Models;

    public class GetTokenQuery : IRequest<RefreshToken>
    {
        public GetTokenQuery(string token)
        {
            Token = token;
        }

        public GetTokenQuery(int userId)
        {
            UserId = userId;
        }

        public string Token { get; set; }

        public int UserId { get; set; }
    }
}
