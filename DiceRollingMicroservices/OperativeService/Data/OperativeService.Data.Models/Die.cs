namespace OperativeService.Data.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Die : BaseModel
    {
        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public DieType DieType { get; set; }
    }
}
