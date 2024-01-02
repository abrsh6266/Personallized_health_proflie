namespace PersonalizedHealthCenter.Settings
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string HealthProfileCollectionName { get; set; } = null!;
        public string UserCollectionName { get; set; } = null!;
        public string SymptomCollectionName { get; set; } = null!;
        public string ChatMessageCollectionName { get; set; } = null!;
        public string MedicationCollectionName { get; set; } = null!;
    }
}
