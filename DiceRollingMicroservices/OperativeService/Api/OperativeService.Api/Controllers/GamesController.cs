namespace OperativeService.Api.Controllers
{
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
                CreateGameCommand command = mapper.Map<CreateGameCommand>(request);
                EntityResponse response = await mediator.Send(command);

                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return BadRequest(response);
            }

            return BadRequest(ModelState);
        }

        [HttpPost(nameof(Join))]
        public async Task<IActionResult> Join([FromBody] JoinGameRequest request)
        {
            if (ModelState.IsValid)
            {
                GameCommand command = mapper.Map<GameCommand>(request);
                BaseResponse response = await mediator.Send(command);

                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return BadRequest(response);
            }

            return BadRequest(ModelState);
        }
    }
}
