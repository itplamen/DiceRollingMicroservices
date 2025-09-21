namespace OperativeService.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using AutoMapper;
    
    using MediatR;
    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    using OperativeService.Api.Models;
    using OperativeService.Handlers.Queries.Response;
    using OperativeService.Handlers.Queries.Users;
    
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public ProfileController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet(nameof(Get))]
        public async Task<IActionResult> Get([FromQuery] ProfileRequest request)
        {
            GetUserProfileQuery query = mapper.Map<GetUserProfileQuery>(request);
            query.ExternalId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            ProfileResponse response = await mediator.Send(query);

            return Ok(response);
        }
    }
}
