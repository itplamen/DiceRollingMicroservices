namespace OperativeService.Handlers.Queries.Response
{
    public class RoundResponse
    {
        public int RoundNumber { get; set; }

        public IEnumerable<int> DiceRolls { get; set; }
    }
}
