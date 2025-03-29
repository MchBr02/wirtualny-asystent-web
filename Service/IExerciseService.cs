using Client_ui.Domain.DTO;
using System.Threading.Tasks;

namespace Client_ui.Service
{
    public interface IExerciseService
    {
        Task<ExerciseDTOs> GetExercise(Guid id);
        Task<IEnumerable<ExerciseDTOs>> GetExercisesForWorkout(Guid workoutId);
        Task<ExerciseDTOs> CreateExercise(Client_ui.Domain.DTO.AddExercises exercise);
        Task<ExerciseDTOs> UpdateExercise(EditExercises exercise);
        Task DeleteExercise(Guid id);
        Task<IEnumerable<ExerciseDTOs>> GetAllExercises();
        Task<Dictionary<string, float>> GetExerciseProgress(string exerciseName);
        //Task<Dictionary<string, float>> GetCategoryVolumeDistribution();
        Task<Dictionary<string, int>> GetExerciseFrequency();
        //Task<Dictionary<string, int>> GetMonthlyWorkoutCount();
        Task<Dictionary<string, float>> GetExerciseVolumeProgress(string exerciseName);
        Task<IEnumerable<ExerciseDTOs>> GetAllExercisesByWorkoutId(Guid workoutId);
        Task<float> GetWorkoutVolume(Guid workoutId);
        Task<TimeSpan> GetTotalExerciseTime(Guid workoutId);
        IEnumerable<string> GetExercisesForCategory(string category);
    }
}
