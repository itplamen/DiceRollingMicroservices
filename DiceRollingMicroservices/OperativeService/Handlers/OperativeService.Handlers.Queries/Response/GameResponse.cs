namespace OperativeService.Handlers.Queries.Response
{
    public class GameResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string DieType { get; set; }

        public int MaxUsers { get; set; }

        public int MaxRounds { get; set; }

        public int DicePerUser { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<UserResponse> Users{ get; set; }

        public IEnumerable<RoundResponse> Rounds { get; set; }
    }
}
