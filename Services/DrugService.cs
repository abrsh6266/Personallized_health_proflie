using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Settings;

namespace PersonalizedHealthCenter.Services
{
    public class DrugService
    {
        private readonly IMongoCollection<Drug> _drugCollection;

        public DrugService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _drugCollection = database.GetCollection<Drug>(mongoDBSettings.Value.DrugCollectionName);
        }

        public async Task<List<Drug>> GetAllAsync()
        {
            return await _drugCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<Drug>> GetByNameAsync(string name)
        {
            var filter = Builders<Drug>.Filter.Eq(nameof(Drug.Name), name);
            return await _drugCollection.Find(filter).ToListAsync();
        }
        public async Task CreateAsync(Drug drug)
        {
            await _drugCollection.InsertOneAsync(drug);
        }
    }
}
