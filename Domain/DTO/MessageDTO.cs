using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace Client_ui.Domain.DTO
{
    public class MessageDto
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("message_id")]
        public string MessageId { get; set; }

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("sender")]
        public string Sender { get; set; }

        [BsonElement("receiver")]
        public string Receiver { get; set; }

        [BsonElement("platform")]
        public string Platform { get; set; }
    }

}
