namespace OperativeService.Handlers.Commands.Response
{
    using DiceRollingMicroservices.Common.Models.Response;

    public class RollDiceResponse : BaseResponse
    {
        public RollDiceResponse() { }

        public RollDiceResponse(string errorMessage)
            : base(errorMessage) { }

        public int RoundNumber { get; set; }

        public IEnumerable<int> DiceRolls { get; set; }

        public int Total => DiceRolls.Sum();
    }
}
