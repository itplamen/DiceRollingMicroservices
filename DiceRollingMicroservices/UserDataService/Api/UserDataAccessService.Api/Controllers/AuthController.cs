namespace UserDataAccessService.Api.Controllers
{
    using AutoMapper;
    
    using MediatR;
    
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    
    using UserDataAccessService.Api.Models;
    using UserDataAccessService.Handlers.Commands.Login;
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
            if (ModelState.IsValid)
            {
                RegisterUserCommand command = mapper.Map<RegisterUserCommand>(request);
                BaseResponse response = await mediator.Send(command);
                
                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return BadRequest(response);
            }

            return BadRequest(ModelState);
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            if (ModelState.IsValid)
            {
                LoginUserCommand command = mapper.Map<LoginUserCommand>(request);
                TokenResponse response = await mediator.Send(command);

                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return Unauthorized(response);

            }

            return BadRequest(ModelState);
        }

        [HttpPost(nameof(RefreshToken))]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (ModelState.IsValid)
            {
                CreateTokenCommand command = mapper.Map<CreateTokenCommand>(request);
                TokenResponse response = await mediator.Send(command);

                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return Unauthorized(response);
            }

            return BadRequest(ModelState);
        }

        [HttpPost(nameof(Logout))]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
        {
            if (ModelState.IsValid)
            {
                RevokeTokenCommand command = mapper.Map<RevokeTokenCommand>(request);
                await mediator.Send(command);

                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}
