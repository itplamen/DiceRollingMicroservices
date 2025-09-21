namespace DiceRollingMicroservices.MessageBus.Producer.Contracts
{
    using DiceRollingMicroservices.MessageBus.Models.Contracts;

    public interface IMessageBusClient<TMessage>
        where TMessage : class, IMessage
    {
        Task Publish(TMessage msg);
    }
}
