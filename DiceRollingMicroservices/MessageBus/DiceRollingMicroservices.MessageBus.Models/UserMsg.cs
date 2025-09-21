namespace DiceRollingMicroservices.MessageBus.Models
{
    using DiceRollingMicroservices.MessageBus.Models.Contracts;

    public class UserMsg : IMessage
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }
    }
}
