using Client_ui.Domain.DTO;

namespace Client_ui.Service
{
    public interface IMongoDbService
    {
        Task<List<MessageDto>> GetMessagesAsync();
        Task<List<MessageDto>> GetMessagesBySenderAsync(string sender);
        Task AddMessageAsync(MessageDto message);
        Task<bool> TestConnectionAsync();
    }

}
