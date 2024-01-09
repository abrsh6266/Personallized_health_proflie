using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalizedHealthCenter.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public List<ChatMessage>? ChatHistory { get; set; }
    }
}
