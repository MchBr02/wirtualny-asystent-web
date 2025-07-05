using Client_ui.Domain.DTO;

namespace Client_ui.Service
{
    public interface IApiChatService
    {
        Task<ApiResponseDto> SendMessageAsync(string message, string? userId = null);
        Task<bool> TestConnectionAsync();
    }
}