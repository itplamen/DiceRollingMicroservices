namespace UserDataAccessService.Handlers.Commands.Register
{
    using AutoMapper;
    
    using MediatR;

    using Microsoft.AspNetCore.Identity;

    using DiceRollingMicroservices.Common.Models.Response;
    using DiceRollingMicroservices.MessageBus.Models;
    using DiceRollingMicroservices.MessageBus.Producer.Contracts;
    using UserDataAccessService.Data.Models;
    using UserDataAccessService.Handlers.Commands.Images;

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseResponse>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;
        private readonly IMessageBusClient<UserMsg> busClient;

        public RegisterUserCommandHandler(IMapper mapper, IMediator mediator, UserManager<User> userManager, IMessageBusClient<UserMsg> busClient)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.userManager = userManager;
            this.busClient = busClient;
        }

        public async Task<BaseResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            User user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                string filePath = await mediator.Send(new UploadImageCommand() { Image = request.Image });

                User newUser = mapper.Map<User>(request);
                newUser.CreatedOn = DateTime.UtcNow;
                newUser.ImageUrl = filePath;

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
