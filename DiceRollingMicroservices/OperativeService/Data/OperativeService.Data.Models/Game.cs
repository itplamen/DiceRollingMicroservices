using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

        [BsonRepresentation(BsonType.String)]
        public DieType DieType { get; set; }

        public int MaxUsers { get; set; }

        public int MaxRounds { get; set; }

        public int DicePerUser { get; set; }

        public ICollection<string> UserIds { get; set; }

        public ICollection<string> RoundIds { get; set; }
    }
}
