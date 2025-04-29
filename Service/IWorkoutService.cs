using Client_ui.Domain.DTO;


namespace Client_ui.Service
{
    public interface IWorkoutService
    {
        Task<IEnumerable<WorkoutDTOs>> GetAllWorkoutDTOsAsync();
        Task<WorkoutDTOs> GetAllWorkoutDTOsByIdAsync(Guid id);
        Task CreteWorkoutAsync(AddWorkoutDTO addWorkoutDTO);
        Task UpdateWorkoutAsync(UpdateWorkoutDTO updateWorkoutDTO);
        Task DeleteWorkoutAsync(Guid id);
        Task<IEnumerable<WorkoutDTOs>> GetWorkoutsByUserIdAsync(Guid userId);
        Task<Dictionary<string, int>> GetMonthlyWorkoutCountAsync();

    }
}
