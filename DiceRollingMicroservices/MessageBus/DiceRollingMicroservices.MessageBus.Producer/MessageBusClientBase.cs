namespace DiceRollingMicroservices.MessageBus.Producer
{
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    using DiceRollingMicroservices.MessageBus.Models.Contracts;
    using DiceRollingMicroservices.MessageBus.Producer.Contracts;
    
    public abstract class MessageBusClientBase<TMessage> : IMessageBusClient<TMessage>, IDisposable
        where TMessage : class, IMessage
    {
        private readonly IConnectionFactory connectionFactory;

        public MessageBusClientBase(IConnectionFactory connectionFactory, string exchangeName)
        {
            this.connectionFactory = connectionFactory;
            this.ExchangeName = exchangeName;
            this.Properties = new BasicProperties()
            {
                ContentType = "text/plain",
                DeliveryMode = DeliveryModes.Persistent
            };
        }

        protected IConnection Connection { get; private set; }

        protected IChannel Channel { get; private set; }

        protected BasicProperties Properties { get; private set; }

        protected string ExchangeName { get; private set; }

        public void Dispose()
        {
            Channel?.CloseAsync().GetAwaiter().GetResult();
            Connection?.CloseAsync().GetAwaiter().GetResult();
        }

        public abstract Task Publish(TMessage msg);

        public abstract Task OnConnectionShutdown(object sender, ShutdownEventArgs e);

        protected async Task Init()
        {
            Connection = await connectionFactory.CreateConnectionAsync();
            Channel = await Connection.CreateChannelAsync();

            await Channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout, durable: true);

            Connection.ConnectionShutdownAsync += OnConnectionShutdown;
        }
    }
}
