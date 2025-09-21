namespace DiceRollingMicroservices.MessageBus.Consumer.Contracts
{
    public interface IEventProcessor
    {
        Task Process(string msg); 
    }
}
