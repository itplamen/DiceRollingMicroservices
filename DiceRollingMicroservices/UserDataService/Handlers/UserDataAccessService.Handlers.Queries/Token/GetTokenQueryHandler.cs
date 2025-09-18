namespace UserDataAccessService.Handlers.Queries.Token
{
    using MediatR;
    
    using Microsoft.EntityFrameworkCore;

    using UserDataAccessService.Data.Contracts;
    using UserDataAccessService.Data.Models;

    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, RefreshToken>
    {
        private readonly IRepository<RefreshToken> repository;

        public GetTokenQueryHandler(IRepository<RefreshToken> repository)
        {
            this.repository = repository;
        }

        public async Task<RefreshToken> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            RefreshToken refreshToken = await repository.Filter()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => 
                    x.Token == request.Token && x.ExpiryDate > DateTime.UtcNow && x.DeletedOn == null, 
                    cancellationToken);

            return refreshToken;
        }
    }
}
