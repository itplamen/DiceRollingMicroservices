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
            IQueryable<RefreshToken> query = repository.Filter()
                .Include(x => x.User)
                .Where(x => x.ExpiryDate > DateTime.UtcNow && x.DeletedOn == null);

            if (!string.IsNullOrEmpty(request.Token))
            {
                query = query.Where(x => x.Token == request.Token);
            }

            if (request.UserId > 0)
            {
                query = query.Where(x => x.UserId == request.UserId);
            }

            RefreshToken refreshToken = await query.FirstOrDefaultAsync(cancellationToken);

            return refreshToken;
        }
    }
}
