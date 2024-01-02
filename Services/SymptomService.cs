using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Settings;
using System.Threading.Tasks;

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

        public async Task CreateAsync(Symptom symptom)
        {
            await _symptomCollection.InsertOneAsync(symptom);
        }

        public async Task<List<Symptom>> GetAsync()
        {
            return await _symptomCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Symptom> GetByIdAsync(string id)
        {
            var filter = Builders<Symptom>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _symptomCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, Symptom updatedSymptom)
        {
            var filter = Builders<Symptom>.Filter.Eq("_id", ObjectId.Parse(id));
            await _symptomCollection.ReplaceOneAsync(filter, updatedSymptom);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Symptom>.Filter.Eq("_id", ObjectId.Parse(id));
            await _symptomCollection.DeleteOneAsync(filter);
        }
    }
}
