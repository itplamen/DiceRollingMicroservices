namespace UserDataAccessService.Handlers.Commands.Logout
{
    using System.Threading.Tasks;

    using MediatR;

    using UserDataAccessService.Data.Models;
    using UserDataAccessService.Handlers.Commands.Response;
    using UserDataAccessService.Handlers.Commands.Token;
    using UserDataAccessService.Handlers.Queries.Token;

    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, BaseResponse>
    {
        private readonly IMediator mediator;

        public LogoutUserCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<BaseResponse> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            RefreshToken refreshToken = await mediator.Send(new GetTokenQuery() { Token = request.RefreshToken });

            if (refreshToken != null)
            {
                await mediator.Send(new RevokeTokenCommand(refreshToken));

                return new BaseResponse();
            }

            return new BaseResponse("Invalid refresh token!");
        }
    }
}
