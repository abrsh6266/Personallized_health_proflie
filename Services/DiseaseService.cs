using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Settings;

namespace PersonalizedHealthCenter.Services
{
    public class DiseaseService
    {
        private readonly IMongoCollection<Disease> _diseaseCollection;

        public DiseaseService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _diseaseCollection = database.GetCollection<Disease>(mongoDBSettings.Value.DiseaseCollectionName);
        }
        public async Task<List<Disease>> GetAsync()
        {
            return await _diseaseCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task<List<Disease>> GetByNameAsync(string name)
        {
            var filter = Builders<Disease>.Filter.Eq(nameof(Disease.Name), name);
            return await _diseaseCollection.Find(filter).ToListAsync();
        }
        public async Task CreateAsync(Disease disease)
        {
            await _diseaseCollection.InsertOneAsync(disease);
        }
    }
}
