namespace UserDataAccessService.Handlers.Commands.Logout
{
    using MediatR;

    using UserDataAccessService.Handlers.Commands.Response;

    public class LogoutUserCommand : IRequest<BaseResponse>
    {
        public string RefreshToken { get; set; }
    }
}
