namespace UserDataAccessService.Handlers.Commands.Token
{
    using MediatR;

    using UserDataAccessService.Data.Contracts;
    using UserDataAccessService.Data.Models;

    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
    {
        private readonly IRepository<RefreshToken> repository;

        public RevokeTokenCommandHandler(IRepository<RefreshToken> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            repository.Delete(request.RefreshToken);
            await repository.SaveChangesAsync(cancellationToken);
        }
    }
}
