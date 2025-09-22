namespace UserDataAccessService.Infrastructure.IoC.Packages
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
 
    using RabbitMQ.Client;

    using DiceRollingMicroservices.Common.Models.IoC;
    using DiceRollingMicroservices.MessageBus.Models;
    using DiceRollingMicroservices.MessageBus.Producer;
    using DiceRollingMicroservices.MessageBus.Producer.Contracts;

    public sealed class MessageBusPackage : IPackage
    {
        private readonly IConfiguration configuration;

        public MessageBusPackage(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IMessageBusClient<UserMsg>>(sp =>
            {
                var connectionFactory = sp.GetRequiredService<IConnectionFactory>();
                string exchangeName = configuration["RabbitMQ:Exchange"];

                var concreteClient = new MessageBusClient<UserMsg>(connectionFactory, exchangeName);
                var logger = sp.GetRequiredService<ILogger<MessageBusClientDecorator<UserMsg>>>();

                return new MessageBusClientDecorator<UserMsg>(connectionFactory, exchangeName, concreteClient, logger);
            });

            services.AddSingleton<IConnectionFactory, ConnectionFactory>(x =>
            {
                return new ConnectionFactory
                {
                    HostName = configuration["RabbitMQ:Host"],
                    Port = int.Parse(configuration["RabbitMQ:Port"])
                };
            });
        }
    }
}
