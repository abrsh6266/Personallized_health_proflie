using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Settings;
namespace PersonalizedHealthCenter.Services
{
    public class HealthProfileService
    {
        private readonly IMongoCollection<HealthProfile> _healthProfileCollection;

        public HealthProfileService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _healthProfileCollection = database.GetCollection<HealthProfile>(mongoDBSettings.Value.HealthProfileCollectionName);
        }

        public async Task CreateAsync(HealthProfile healthProfile)
        {
            await _healthProfileCollection.InsertOneAsync(healthProfile);
        }

        public async Task<List<HealthProfile>> GetAsync()
        {
            return await _healthProfileCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<HealthProfile> GetByIdAsync(string id)
        {
            var filter = Builders<HealthProfile>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _healthProfileCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, HealthProfile updatedHealthProfile)
        {
            var filter = Builders<HealthProfile>.Filter.Eq("_id", ObjectId.Parse(id));
            await _healthProfileCollection.ReplaceOneAsync(filter, updatedHealthProfile);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<HealthProfile>.Filter.Eq("_id", ObjectId.Parse(id));
            await _healthProfileCollection.DeleteOneAsync(filter);
        }
    }
}
