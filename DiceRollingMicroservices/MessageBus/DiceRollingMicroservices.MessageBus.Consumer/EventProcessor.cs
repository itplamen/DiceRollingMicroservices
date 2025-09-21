namespace DiceRollingMicroservices.MessageBus.Consumer
{
    using System.Text.Json;

    using AutoMapper;

    using MediatR;
    
    using Microsoft.Extensions.DependencyInjection;

    using DiceRollingMicroservices.MessageBus.Consumer.Contracts;
    using DiceRollingMicroservices.MessageBus.Models;
    using OperativeService.Handlers.Commands.Users;

    public class EventProcessor : IEventProcessor
    {
        private readonly IMapper mapper;
        private readonly IServiceScopeFactory scopeFactory;

        public EventProcessor(IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            this.mapper = mapper;
            this.scopeFactory = scopeFactory;
        }

        public async Task Process(string msg)
        {
            UserMsg userMsg = JsonSerializer.Deserialize<UserMsg>(msg);
            var command = mapper.Map<CreateUserComman>(userMsg);

            using var scope = scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(command);
        }
    }
}
