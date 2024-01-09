using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalizedHealthCenter.Models
{
    public class ChatMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string? UserPrompt { get; set; }
        public string? UserId { get; set; }
        public string? Response { get; set; }
    }
}
