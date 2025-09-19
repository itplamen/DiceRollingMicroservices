namespace OperativeService.Data.Models
{
    public class RollResult
    {
        public string UserId { get; set; } 

        public ICollection<int> DiceRolls { get; set; } = new List<int>();
    }
}
