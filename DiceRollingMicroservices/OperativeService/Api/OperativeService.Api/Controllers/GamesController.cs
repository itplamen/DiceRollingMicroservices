namespace OperativeService.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using AutoMapper;
    
    using MediatR;
    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using DiceRollingMicroservices.Common.Models.Response;
    using OperativeService.Api.Models;
    using OperativeService.Handlers.Commands.Common;
    using OperativeService.Handlers.Commands.Games;
    using OperativeService.Handlers.Commands.Response;
    using OperativeService.Handlers.Queries.Users;

    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public GamesController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        //[HttpGet(nameof(Get))]
        //public async Task<IActionResult> Get()
        //{

        //}

        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create([FromBody] CreateGameRequest request)
        {
            if (ModelState.IsValid)
            {
                EntityResponse userResponse = await GetUserId();

                if (userResponse.IsSuccess)
                {
                    CreateGameCommand command = mapper.Map<CreateGameCommand>(request);
                    command.UserId = userResponse.Id;

                    EntityResponse response = await mediator.Send(command);

                    if (response.IsSuccess)
                    {
                        return Ok(response);
                    }

                    return BadRequest(response);
                }

                return BadRequest(userResponse);
            }

            return BadRequest(ModelState);
        }

        [HttpPost(nameof(Join))]
        public async Task<IActionResult> Join([FromBody] GameRequest request)
        {
            if (ModelState.IsValid)
            {
                EntityResponse userResponse = await GetUserId();

                if (userResponse.IsSuccess)
                {
                    GameCommand command = mapper.Map<GameCommand>(request);
                    command.UserId = userResponse.Id;

                    BaseResponse response = await mediator.Send(command);

                    if (response.IsSuccess)
                    {
                        return Ok(response);
                    }

                    return BadRequest(response);
                }

                return BadRequest(userResponse);
            }

            return BadRequest(ModelState);
        }

        [HttpPost(nameof(Play))]
        public async Task<IActionResult> Play([FromBody] GameRequest request)
        {
            if (ModelState.IsValid)
            {
                EntityResponse userResponse = await GetUserId();

                if (userResponse.IsSuccess)
                {
                    GameCommand command = mapper.Map<GameCommand>(request);
                    command.UserId = userResponse.Id;

                    RollDiceResponse response = await mediator.Send(command);

                    if (response.IsSuccess)
                    {
                        return Ok(response);
                    }

                    return BadRequest(response);
                }

                return BadRequest(userResponse);
            }

            return BadRequest(ModelState);
        }

        private async Task<EntityResponse> GetUserId()
        {
            int externalUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            EntityResponse userResponse = await mediator.Send(new GetUserQuery() { ExternalId = externalUserId });

            return userResponse;
        }
    }
}
