using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalizedHealthCenter.Models
{
    public class ChatMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public string? SenderId { get; set; }
        public string? Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
