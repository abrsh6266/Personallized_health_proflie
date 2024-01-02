using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Settings;
using System.Threading.Tasks;

namespace PersonalizedHealthCenter.Services
{
    public class MedicationService
    {
        private readonly IMongoCollection<Medication> _medicationCollection;

        public MedicationService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _medicationCollection = database.GetCollection<Medication>(mongoDBSettings.Value.MedicationCollectionName);
        }

        public async Task CreateAsync(Medication medication)
        {
            await _medicationCollection.InsertOneAsync(medication);
        }

        public async Task<List<Medication>> GetAsync()
        {
            return await _medicationCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Medication> GetByIdAsync(string id)
        {
            var filter = Builders<Medication>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _medicationCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, Medication updatedMedication)
        {
            var filter = Builders<Medication>.Filter.Eq("_id", ObjectId.Parse(id));
            await _medicationCollection.ReplaceOneAsync(filter, updatedMedication);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Medication>.Filter.Eq("_id", ObjectId.Parse(id));
            await _medicationCollection.DeleteOneAsync(filter);
        }
    }
}
