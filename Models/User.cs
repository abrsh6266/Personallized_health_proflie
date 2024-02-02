using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace PersonalizedHealthCenter.Models
{
    [CollectionName("users")]
    public class User: MongoIdentityUser<Guid>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public List<ChatMessage>? ChatHistory { get; set; }
    }
}
