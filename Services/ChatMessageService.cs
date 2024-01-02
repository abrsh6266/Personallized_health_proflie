using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PersonalizedHealthCenter.Models;
using PersonalizedHealthCenter.Settings;
using System.Threading.Tasks;

namespace PersonalizedHealthCenter.Services
{
    public class ChatMessageService
    {
        private readonly IMongoCollection<ChatMessage> _chatMessageCollection;

        public ChatMessageService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _chatMessageCollection = database.GetCollection<ChatMessage>(mongoDBSettings.Value.ChatMessageCollectionName);
        }

        public async Task CreateAsync(ChatMessage chatMessage)
        {
            await _chatMessageCollection.InsertOneAsync(chatMessage);
        }

        public async Task<List<ChatMessage>> GetAsync()
        {
            return await _chatMessageCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<ChatMessage> GetByIdAsync(string id)
        {
            var filter = Builders<ChatMessage>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _chatMessageCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, ChatMessage updatedChatMessage)
        {
            var filter = Builders<ChatMessage>.Filter.Eq("_id", ObjectId.Parse(id));
            await _chatMessageCollection.ReplaceOneAsync(filter, updatedChatMessage);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<ChatMessage>.Filter.Eq("_id", ObjectId.Parse(id));
            await _chatMessageCollection.DeleteOneAsync(filter);
        }
    }
}
