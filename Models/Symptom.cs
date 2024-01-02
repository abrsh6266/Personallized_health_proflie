using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalizedHealthCenter.Models
{
    public class Symptom
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string? SymptomName { get; set; }
        public string? Description { get; set; }
    }
}
