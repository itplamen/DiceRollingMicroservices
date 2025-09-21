namespace OperativeService.Api.Controllers
{
    using System;
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

        [HttpPost(nameof(Create))]
        public Task<IActionResult> Create([FromBody] CreateGameRequest request) =>
            HandleRequest(request, (req, userId) =>
            {
                var command = mapper.Map<CreateGameCommand>(req);
                command.UserId = userId;
                return mediator.Send(command);
            });

        [HttpPost(nameof(Join))]
        public Task<IActionResult> Join([FromBody] GameRequest request) =>
            HandleRequest(request, (req, userId) =>
            {
                var command = mapper.Map<GameCommand>(req);
                command.UserId = userId;
                return mediator.Send(command);
            });

        [HttpPost(nameof(Play))]
        public Task<IActionResult> Play([FromBody] GameRequest request) =>
            HandleRequest(request, (req, userId) =>
            {
                var command = mapper.Map<GameCommand>(req);
                command.UserId = userId;

                return mediator.Send(command);
            });

        private async Task<IActionResult> HandleRequest<TRequest, TResponse>(TRequest request, Func<TRequest, string, Task<TResponse>> handler)
            where TRequest : class
            where TResponse : IResponse
        {
            int externalUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            EntityResponse userResponse = await mediator.Send(new GetUserQuery() { ExternalId = externalUserId });

            if (!userResponse.IsSuccess)
            {
                return BadRequest(userResponse);
            }

            TResponse response = await handler(request, userResponse.Id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
