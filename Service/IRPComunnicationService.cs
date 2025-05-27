using Client_ui.Domain.DTO;
namespace Client_ui.Service
{
    public interface IRPComunnicationService
    {
        Task<TrainingResponseDto> SendMessageAsync(string message, string userId = "blazor_user");
        Task<UserStatsDto?> GetUserStatsAsync(string userId);
        Task<bool> TestConnectionAsync();
    }
}
