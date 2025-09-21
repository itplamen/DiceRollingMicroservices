namespace OperativeService.Infrastructure.IoC.Packages
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    
    using RabbitMQ.Client;

    using DiceRollingMicroservices.Common.Models.IoC;
    using DiceRollingMicroservices.MessageBus.Consumer;
    using DiceRollingMicroservices.MessageBus.Consumer.Contracts;
    
    public sealed class MessageBusPackage : IPackage
    {
        private readonly IConfiguration configuration;

        public MessageBusPackage(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IEventProcessor, EventProcessor>();
            services.AddSingleton<IConnectionFactory, ConnectionFactory>(x =>
            {
                return new ConnectionFactory
                {
                    HostName = configuration["RabbitMQ:Host"],
                    Port = int.Parse(configuration["RabbitMQ:Port"])
                };
            });

            services.AddHostedService(x =>
            {
                var eventProcessor = x.GetRequiredService<IEventProcessor>();
                var connectionFactory = x.GetRequiredService<IConnectionFactory>();
                string exchangeName = configuration["RabbitMQ:Exchange"];
                string queueName = configuration["RabbitMQ:Queue"];

                return new MessageBusSubscriber(eventProcessor, connectionFactory, exchangeName, queueName);
            });
        }
    }
}
