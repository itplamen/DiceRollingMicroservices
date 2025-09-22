namespace DiceRollingMicroservices.MessageBus.Producer
{
    using Microsoft.Extensions.Logging;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    using DiceRollingMicroservices.MessageBus.Models.Contracts;
    using DiceRollingMicroservices.MessageBus.Producer.Contracts;

    public class MessageBusClientDecorator<TMessage> : MessageBusClientBase<TMessage>
        where TMessage : class, IMessage
    {
        private readonly IMessageBusClient<TMessage> decoratee;
        private readonly ILogger<MessageBusClientDecorator<TMessage>> logger;

        public MessageBusClientDecorator(
            IConnectionFactory connectionFactory, 
            string exchangeName, 
            IMessageBusClient<TMessage> decoratee, 
            ILogger<MessageBusClientDecorator<TMessage>> logger)
            : base(connectionFactory, exchangeName)
        {
            this.decoratee = decoratee;
            this.logger = logger;
        }

        public override async Task Publish(TMessage msg)
        {
            try
            {
                await decoratee.Publish(msg);

                logger.LogInformation($"Message Sent [{DateTime.UtcNow}] - {msg}");
            }
            catch (Exception ex)
            {
                logger.LogError($"ERROR: Message Sent [{DateTime.UtcNow}] - {msg}", ex);
            }
        }

        public override async Task OnConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            try
            {
                await decoratee.OnConnectionShutdown(sender, e);

                logger.LogInformation($"Successfull SHUTDOWN: [{DateTime.UtcNow}]");
            }
            catch (Exception ex)
            {
                logger.LogError($"ERROR: SHUTDOWN [{DateTime.UtcNow}]", ex);
            }
        }
    }
}
