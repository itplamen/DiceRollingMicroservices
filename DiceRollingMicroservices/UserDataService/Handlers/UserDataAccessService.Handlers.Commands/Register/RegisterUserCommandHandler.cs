namespace UserDataAccessService.Handlers.Commands.Register
{
    using AutoMapper;
    
    using MediatR;

    using Microsoft.AspNetCore.Identity;

    using DiceRollingMicroservices.Common.Models.Response;
    using UserDataAccessService.Data.Models;
    
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseResponse>
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
 
        public RegisterUserCommandHandler(IMapper mapper, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<BaseResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            User user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                User newUser = mapper.Map<User>(request);
                IdentityResult result = await userManager.CreateAsync(newUser, request.Password);
                BaseResponse response = mapper.Map<BaseResponse>(result);

                return response;
            }

            return new BaseResponse("Username already exists");
        }
    }
}
