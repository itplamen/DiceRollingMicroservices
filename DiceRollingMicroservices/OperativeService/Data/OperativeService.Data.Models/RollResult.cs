namespace OperativeService.Data.Models
{
    public class RollResult : BaseModel
    {
        public RollResult()
        {
            DiceRolls = new HashSet<DieRoll>(ReferenceEqualityComparer.Instance);
        }

        public string UserId { get; set; } 

        public int Total => DiceRolls.Sum(x => x.Result);

        public ICollection<DieRoll> DiceRolls { get; set; }
    }
}
