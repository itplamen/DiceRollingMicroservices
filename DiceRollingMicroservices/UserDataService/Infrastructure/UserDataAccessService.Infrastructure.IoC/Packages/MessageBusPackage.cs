namespace UserDataAccessService.Infrastructure.IoC.Packages
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using RabbitMQ.Client;

    using DiceRollingMicroservices.Common.Models.IoC;
    using DiceRollingMicroservices.MessageBus.Producer;
    using DiceRollingMicroservices.MessageBus.Producer.Contracts;
    using DiceRollingMicroservices.MessageBus.Models;

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
                var exchangeName = configuration["RabbitMQ:Exchange"];

                return new MessageBusClient<UserMsg>(connectionFactory, exchangeName);
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
