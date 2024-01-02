using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace PersonalizedHealthCenter.Models
{
    public class Medication
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string? MedicationName { get; set; } // Non-nullable
        public string? Dosage { get; set; } // Nullable property
        public string? Schedule { get; set; } // Nullable property
    }
}
