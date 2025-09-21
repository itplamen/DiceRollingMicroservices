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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (connection == null || !connection.IsOpen)
            {
                await Init();
            }

            Console.WriteLine("Listenning ...");
 
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
                    Console.WriteLine($"Error processing message: {ex.Message}");

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

            connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdown;

            var queue = await channel.QueueDeclareAsync(
               queue: queueName,
               durable: true,
               exclusive: false,
               autoDelete: false);

            await channel.QueueBindAsync(queue.QueueName, exchangeName, "");
        }

        private Task RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            return Task.CompletedTask;
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
    }
}
