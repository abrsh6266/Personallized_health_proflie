using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalizedHealthCenter.Models
{
    public class HealthProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string MedicalHistory { get; set; } = null!;
        public string Allergies { get; set; } = null!;
        public List<Medication> Medications { get; set; } = null!;
    }
}
