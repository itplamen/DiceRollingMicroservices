namespace UserDataAccessService.Handlers.Commands.Logout
{
    using MediatR;

    using DiceRollingMicroservices.Common.Models.Response;
    
    public class LogoutUserCommand : IRequest<BaseResponse>
    {
        public string RefreshToken { get; set; }
    }
}
