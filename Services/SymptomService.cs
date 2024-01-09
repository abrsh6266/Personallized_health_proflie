using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Settings;

namespace PersonalizedHealthCenter.Services
{
    public class SymptomService
    {
        private readonly IMongoCollection<Symptom> _symptomCollection;

        public SymptomService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _symptomCollection = database.GetCollection<Symptom>(mongoDBSettings.Value.SymptomCollectionName);
        }

        public async Task<List<Symptom>> GetAllAsync()
        {
            return await _symptomCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<Symptom>> GetByNameAsync(string name)
        {
            var filter = Builders<Symptom>.Filter.Eq(nameof(Symptom.Name), name);
            return await _symptomCollection.Find(filter).ToListAsync();
        }
        public async Task CreateAsync(Symptom symptom)
        {
            await _symptomCollection.InsertOneAsync(symptom);
        }

    }
}
