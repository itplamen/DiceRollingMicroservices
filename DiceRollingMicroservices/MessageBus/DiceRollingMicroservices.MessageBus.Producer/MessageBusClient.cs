namespace DiceRollingMicroservices.MessageBus.Producer
{
    using System.Text;
    using System.Text.Json;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    using DiceRollingMicroservices.MessageBus.Models.Contracts;

    public class MessageBusClient<TMessage> : MessageBusClientBase<TMessage>
        where TMessage : class, IMessage
    {
        public MessageBusClient(IConnectionFactory connectionFactory, string exchangeName) 
            : base(connectionFactory, exchangeName)
        {
        }

        public override async Task Publish(TMessage msg)
        {
            if (Connection == null || !Connection.IsOpen)
            {
                await Init();
            }

            string message = JsonSerializer.Serialize(msg);
            Memory<byte> body = Encoding.UTF8.GetBytes(message).AsMemory();

            await Channel.BasicPublishAsync(
                exchange: ExchangeName,
                routingKey: "",
                mandatory: false,
                basicProperties: Properties,
                body: body
            );
        }

        public override Task OnConnectionShutdown(object sender, ShutdownEventArgs e) => Task.CompletedTask;
    }
}
