namespace DiceRollingMicroservices.MessageBus.Consumer
{
    using System.Text;

    using Microsoft.Extensions.Hosting;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    using DiceRollingMicroservices.MessageBus.Consumer.Contracts;

    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly IEventProcessor eventProcessor;
        private readonly string exchangeName;
        private readonly string queueName;
        private IConnection connection;
        private IChannel channel;

        public MessageBusSubscriber(IEventProcessor eventProcessor, IConnectionFactory connectionFactory, string exchangeName, string queueName)
        {
            this.eventProcessor = eventProcessor;
            this.connectionFactory = connectionFactory;
            this.exchangeName = exchangeName;
            this.queueName = queueName;
        }

        public override void Dispose()
        {
            if (channel?.IsOpen ?? false)
            {
                channel.CloseAsync().GetAwaiter().GetResult();
            }

            if (connection?.IsOpen ?? false)
            {
                connection.CloseAsync().GetAwaiter().GetResult();
            }

            base.Dispose();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (connection == null || !connection.IsOpen)
            {
                await Init();
            }
 
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (ModuleHandle, args) =>
            {
                try
                {
                    string msg = Encoding.UTF8.GetString(args.Body.ToArray());
                    await eventProcessor.Process(msg);

                    await channel.BasicAckAsync(args.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    await channel.BasicNackAsync(args.DeliveryTag, multiple: false, requeue: true);
                }
            };

            await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
        }

        private async Task Init()
        {
            connection = await connectionFactory.CreateConnectionAsync();
            channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Fanout, durable: true);

            connection.ConnectionShutdownAsync += OnConnectionShutdown;

            var queue = await channel.QueueDeclareAsync(
               queue: queueName,
               durable: true,
               exclusive: false,
               autoDelete: false);

            await channel.QueueBindAsync(queue.QueueName, exchangeName, "");
        }

        private Task OnConnectionShutdown(object sender, ShutdownEventArgs e) => Task.CompletedTask;
    }
}
