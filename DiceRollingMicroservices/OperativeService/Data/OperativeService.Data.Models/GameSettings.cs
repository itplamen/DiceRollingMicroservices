namespace OperativeService.Data.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class GameSettings : BaseModel
    {
        [BsonRepresentation(BsonType.String)]
        public DieType DieType { get; set; }

        public int MaxPlayers { get; set; }

        public int MaxRounds { get; set; }

        public int DicePerPlayer { get; set; }
    }
}
