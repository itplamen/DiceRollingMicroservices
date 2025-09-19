namespace OperativeService.Data.Models
{
    public class Round : BaseModel
    {
        public Round()
        {
            Results = new HashSet<RollResult>(ReferenceEqualityComparer.Instance);
        }

        public int RoundNumber { get; set; }

        public string GameId { get; set; }

        public ICollection<RollResult> Results { get; set; } 
    }
}
