using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Text.Json;

using Client_ui.Domain.DTO;

namespace Client_ui.Service
{
    public class MongoDbService : IMongoDbService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<MessageDto> _messagesCollection;

        public MongoDbService()
        {
            // Połączenie z MongoDB na Raspberry Pi
            var connectionString = "mongodb://185.118.214.132:27017";
            var client = new MongoClient(connectionString);

            // Nazwa bazy danych z projektu kolegi
            _database = client.GetDatabase("discord_bot");
            _messagesCollection = _database.GetCollection<MessageDto>("messages");
        }

        public async Task<List<MessageDto>> GetMessagesAsync()
        {
            try
            {
                return await _messagesCollection
                    .Find(_ => true)
                    .SortByDescending(x => x.Timestamp)
                    .Limit(50)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas pobierania wiadomości: {ex.Message}");
            }
        }

        public async Task<List<MessageDto>> GetMessagesBySenderAsync(string sender)
        {
            try
            {
                return await _messagesCollection
                    .Find(x => x.Sender == sender)
                    .SortByDescending(x => x.Timestamp)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas pobierania wiadomości dla {sender}: {ex.Message}");
            }
        }

        public async Task AddMessageAsync(MessageDto message)
        {
            try
            {
                message.Timestamp = DateTime.UtcNow;
                await _messagesCollection.InsertOneAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas dodawania wiadomości: {ex.Message}");
            }
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                await _database.RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}