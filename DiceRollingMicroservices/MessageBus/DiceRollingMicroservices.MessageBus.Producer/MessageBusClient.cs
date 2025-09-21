namespace DiceRollingMicroservices.MessageBus.Producer
{
    using System.Text;
    using System.Text.Json;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    using DiceRollingMicroservices.MessageBus.Models.Contracts;
    using DiceRollingMicroservices.MessageBus.Producer.Contracts;

    public class MessageBusClient<TMessage> : IMessageBusClient<TMessage>, IDisposable
        where TMessage : class, IMessage
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly BasicProperties properties;
        private readonly string exchangeName;
        private IConnection connection;
        private IChannel channel;

        public MessageBusClient(IConnectionFactory connectionFactory, string exchangeName)
        {
            this.connectionFactory = connectionFactory;
            this.exchangeName = exchangeName;
            this.properties = new BasicProperties()
            {
                ContentType = "text/plain",
                DeliveryMode = DeliveryModes.Persistent
            };
        }

        public async Task Publish(TMessage msg)
        {
            try
            {
                if (connection == null || !connection.IsOpen) 
                {
                    await Init();
                }
                
                string message = JsonSerializer.Serialize(msg);
                Memory<byte> body = Encoding.UTF8.GetBytes(message).AsMemory();
 
                await channel.BasicPublishAsync(
                    exchange: exchangeName,
                    routingKey: "",
                    mandatory: false,
                    basicProperties: properties,
                    body: body
                );

                Console.WriteLine("Message Sent!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing message: {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            channel?.CloseAsync().GetAwaiter().GetResult();
            connection?.CloseAsync().GetAwaiter().GetResult();
        }

        private async Task Init()
        {
            connection = await connectionFactory.CreateConnectionAsync();
            channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout, durable: true);

            connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdown;
        }

        private Task RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("RabbitMQ connection shut down.");
            return Task.CompletedTask;
        }
    }
}
