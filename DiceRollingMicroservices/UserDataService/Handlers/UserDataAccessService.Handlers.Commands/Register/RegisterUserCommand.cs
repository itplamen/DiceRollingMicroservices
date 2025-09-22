namespace UserDataAccessService.Handlers.Commands.Register
{
    using MediatR;
    
    using Microsoft.AspNetCore.Http;

    using DiceRollingMicroservices.Common.Models.Response;

    public class RegisterUserCommand  : IRequest<BaseResponse>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IFormFile Image { get; set; }
    }
}
