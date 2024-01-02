using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Settings;
using System.Threading.Tasks;

namespace PersonalizedHealthCenter.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _userCollection = database.GetCollection<User>(mongoDBSettings.Value.UserCollectionName);
        }

        public async Task CreateAsync(User user)
        {
            await _userCollection.InsertOneAsync(user);
        }

        public async Task<List<User>> GetAsync()
        {
            return await _userCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _userCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, User updatedUser)
        {
            var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));
            await _userCollection.ReplaceOneAsync(filter, updatedUser);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(id));
            await _userCollection.DeleteOneAsync(filter);
        }
    }
}
