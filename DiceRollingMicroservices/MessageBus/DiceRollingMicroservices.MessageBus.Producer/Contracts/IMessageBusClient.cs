namespace DiceRollingMicroservices.MessageBus.Producer.Contracts
{
    using RabbitMQ.Client.Events;

    using DiceRollingMicroservices.MessageBus.Models.Contracts;

    public interface IMessageBusClient<TMessage>
        where TMessage : class, IMessage
    {
        Task Publish(TMessage msg);

        Task OnConnectionShutdown(object sender, ShutdownEventArgs e);
    }
}
