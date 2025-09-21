namespace UserDataAccessService.Api.Controllers
{
    using System.Threading.Tasks;

    using AutoMapper;
    
    using MediatR;
    
    using Microsoft.AspNetCore.Mvc;

    using DiceRollingMicroservices.Common.Models.Response;
    using UserDataAccessService.Api.Models;
    using UserDataAccessService.Handlers.Commands.Login;
    using UserDataAccessService.Handlers.Commands.Logout;
    using UserDataAccessService.Handlers.Commands.Register;
    using UserDataAccessService.Handlers.Commands.Response;
    using UserDataAccessService.Handlers.Commands.Token;

    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public AuthController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            RegisterUserCommand command = mapper.Map<RegisterUserCommand>(request);
            BaseResponse response = await mediator.Send(command);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            LoginUserCommand command = mapper.Map<LoginUserCommand>(request);
            TokenResponse response = await mediator.Send(command);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return Unauthorized(response);
        }

        [HttpPost(nameof(RefreshToken))]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            CreateRefreshTokenCommand command = mapper.Map<CreateRefreshTokenCommand>(request);
            TokenResponse response = await mediator.Send(command);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return Unauthorized(response);
        }

        [HttpPost(nameof(Logout))]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
        {
            LogoutUserCommand command = mapper.Map<LogoutUserCommand>(request);
            BaseResponse response = await mediator.Send(command);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return Unauthorized(response);
        }
    }
}
