namespace UserDataAccessService.Handlers.Commands.Register
{
    using MediatR;

    using DiceRollingMicroservices.Common.Models.Response;

    public class RegisterUserCommand  : IRequest<BaseResponse>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ImageUrl { get; set; }
    }
}
