namespace UserDataAccessService.Handlers.Commands.Register
{
    using AutoMapper;
    
    using MediatR;

    using Microsoft.AspNetCore.Identity;

    using DiceRollingMicroservices.Common.Models.Response;
    using DiceRollingMicroservices.MessageBus.Models;
    using DiceRollingMicroservices.MessageBus.Producer.Contracts;
    using UserDataAccessService.Data.Models;

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseResponse>
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly IMessageBusClient<UserMsg> busClient;

        public RegisterUserCommandHandler(IMapper mapper, UserManager<User> userManager, IMessageBusClient<UserMsg> busClient)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.busClient = busClient;
        }

        public async Task<BaseResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            User user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                User newUser = mapper.Map<User>(request);
                IdentityResult result = await userManager.CreateAsync(newUser, request.Password);

                if (result.Succeeded)
                {
                    UserMsg userMsg = mapper.Map<UserMsg>(newUser);
                    await busClient.Publish(userMsg);
                }

                BaseResponse response = mapper.Map<BaseResponse>(result);

                return response;
            }

            return new BaseResponse("Username already exists");
        }
    }
}
