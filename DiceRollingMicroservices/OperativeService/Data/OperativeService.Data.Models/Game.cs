namespace OperativeService.Data.Models
{
    public class Game : BaseModel
    {
        public Game()
        {
            UserIds = new HashSet<string>(ReferenceEqualityComparer.Instance);
            RoundIds = new HashSet<string>(ReferenceEqualityComparer.Instance);
        }

        public string Name { get; set; }

        public string GameSettingsId { get; set; }

        public ICollection<string> UserIds { get; set; }

        public ICollection<string> RoundIds { get; set; }
    }
}
