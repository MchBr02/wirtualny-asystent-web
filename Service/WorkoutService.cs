using Client_ui.Domain;
using Client_ui.Domain.DTO;
using Client_ui.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Client_ui.Service
{

    public class WorkoutService : IWorkoutService
    {
        private readonly WorkoutAppDbContext _workoutAppDbContext;

        public WorkoutService(WorkoutAppDbContext workoutAppDbContext)
        {
            this._workoutAppDbContext = workoutAppDbContext;
        }

        public async Task CreteWorkoutAsync(AddWorkoutDTO addWorkoutDTO)
        {
            var workout = new Workout
            {
                Id = Guid.NewGuid(),
                WorkoutName = addWorkoutDTO.WorkoutName,
                WorkoutDate = addWorkoutDTO.WorkoutDate,
                UserId = addWorkoutDTO.UserId // Add this line
            };
            _workoutAppDbContext.Workouts.Add(workout);
            await _workoutAppDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkoutDTOs>> GetAllWorkoutDTOsAsync()
        {
            try
            {
                var workouts = await _workoutAppDbContext.Workouts
                    .Include(w => w.Exercises)
                    .ToListAsync();

                Console.WriteLine($"Znaleziono {workouts.Count} treningów");

                var result = workouts.Select(w => new WorkoutDTOs
                {
                    Id = w.Id,
                    WorkoutName = w.WorkoutName,
                    WorkoutDate = w.WorkoutDate,
                    WorkoutTime = TimeSpan.FromMinutes(w.Exercises.Sum(e => e.ExerciseTime.TotalMinutes)),
                    WorkoutVolume = w.Exercises.Sum(e => e.ExerciseWeight * e.ExerciseReps * e.ExerciseSets),
                    WorkoutQuality = w.Exercises.Any() ? (int)Math.Round(w.Exercises.Average(e => e.ExerciseQuality)) : 0,
                    Url = $"/WorkoutDetails/{w.WorkoutName}/{w.Id}",
                    UserId = w.UserId, // Add this line
                    Exercises = w.Exercises.Select(ex => new ExerciseDTOs
                    {
                        Id = ex.Id,
                        WorkoutId = ex.WorkoutId,
                        ExerciseName = ex.ExerciseName,
                        ExerciseWeight = ex.ExerciseWeight,
                        ExerciseReps = ex.ExerciseReps,
                        ExerciseSets = ex.ExerciseSets,
                        Category = ex.Category,
                        ExerciseTime = ex.ExerciseTime,
                        ExerciseQuality = ex.ExerciseQuality
                    }).ToList()
                }).ToList();

                Console.WriteLine($"Przekonwertowano {result.Count} treningów na DTO");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas ładowania treningów: {ex.Message}");
                throw;
            }
        }

        // New method to get workouts for a specific user
        public async Task<IEnumerable<WorkoutDTOs>> GetWorkoutsByUserIdAsync(Guid userId)
        {
            try
            {
                var workouts = await _workoutAppDbContext.Workouts
                    .Include(w => w.Exercises)
                    .Where(w => w.UserId == userId) // Filter by user ID
                    .ToListAsync();

                Console.WriteLine($"Znaleziono {workouts.Count} treningów dla użytkownika {userId}");

                var result = workouts.Select(w => new WorkoutDTOs
                {
                    Id = w.Id,
                    WorkoutName = w.WorkoutName,
                    WorkoutDate = w.WorkoutDate,
                    WorkoutTime = TimeSpan.FromMinutes(w.Exercises.Sum(e => e.ExerciseTime.TotalMinutes)),
                    WorkoutVolume = w.Exercises.Sum(e => e.ExerciseWeight * e.ExerciseReps * e.ExerciseSets),
                    WorkoutQuality = w.Exercises.Any() ? (int)Math.Round(w.Exercises.Average(e => e.ExerciseQuality)) : 0,
                    Url = $"/WorkoutDetails/{w.WorkoutName}/{w.Id}",
                    UserId = w.UserId,
                    Exercises = w.Exercises.Select(ex => new ExerciseDTOs
                    {
                        Id = ex.Id,
                        WorkoutId = ex.WorkoutId,
                        ExerciseName = ex.ExerciseName,
                        ExerciseWeight = ex.ExerciseWeight,
                        ExerciseReps = ex.ExerciseReps,
                        ExerciseSets = ex.ExerciseSets,
                        Category = ex.Category,
                        ExerciseTime = ex.ExerciseTime,
                        ExerciseQuality = ex.ExerciseQuality
                    }).ToList()
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas ładowania treningów: {ex.Message}");
                throw;
            }
        }

        public async Task<WorkoutDTOs> GetAllWorkoutDTOsByIdAsync(Guid id)
        {
            var workout = await _workoutAppDbContext.Workouts
                .Include(w => w.Exercises)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workout == null)
            {
                throw new KeyNotFoundException("Workout Not Found");
            }

            var totalVolume = workout.Exercises.Sum(e => e.ExerciseWeight * e.ExerciseReps * e.ExerciseSets);
            var totalTime = workout.Exercises.Sum(e => e.ExerciseTime.TotalMinutes);
            var averageQuality = workout.Exercises.Any() ? workout.Exercises.Average(e => e.ExerciseQuality) : 0;

            return new WorkoutDTOs
            {
                Id = workout.Id,
                WorkoutName = workout.WorkoutName,
                WorkoutDate = workout.WorkoutDate,
                WorkoutTime = TimeSpan.FromMinutes(totalTime),
                WorkoutVolume = totalVolume,
                WorkoutQuality = (int)Math.Round(averageQuality),
                Url = $"/WorkoutDetails/{workout.WorkoutName}/{workout.Id}",
                UserId = workout.UserId, // Add this line
                Exercises = workout.Exercises.Select(ex => new ExerciseDTOs
                {
                    Id = ex.Id,
                    WorkoutId = ex.WorkoutId,
                    ExerciseName = ex.ExerciseName,
                    ExerciseWeight = ex.ExerciseWeight,
                    ExerciseReps = ex.ExerciseReps,
                    ExerciseSets = ex.ExerciseSets,
                    Category = ex.Category,
                    ExerciseTime = ex.ExerciseTime,
                    ExerciseQuality = ex.ExerciseQuality
                }).ToList()
            };
        }

        public async Task UpdateWorkoutAsync(UpdateWorkoutDTO updateWorkoutDTO)
        {
            var workout = await _workoutAppDbContext.Workouts.FindAsync(updateWorkoutDTO.Id);
            {
                if (workout == null)
                {
                    throw new KeyNotFoundException("Workout Not Found");
                }

                // Check if the user is authorized to update this workout
                if (workout.UserId != updateWorkoutDTO.UserId)
                {
                    throw new UnauthorizedAccessException("You are not authorized to update this workout");
                }

                workout.WorkoutName = updateWorkoutDTO.WorkoutName;
                workout.WorkoutDate = updateWorkoutDTO.WorkoutDate;
            }

            await _workoutAppDbContext.SaveChangesAsync(); // Add this line to actually save changes
        }

        public async Task DeleteWorkoutAsync(Guid id)
        {
            var workout = await _workoutAppDbContext.Workouts.FindAsync(id);
            {
                if (workout == null)
                {
                    throw new KeyNotFoundException("Workout Not Found");
                }
            }
            _workoutAppDbContext.Workouts.Remove(workout);
            await _workoutAppDbContext.SaveChangesAsync();
        }

        public async Task<WorkoutDTOs?> GetWorkoutDTOByIdAsync(Guid id)
        {
            var workout = await _workoutAppDbContext.Workouts
                .Include(w => w.Exercises)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workout == null)
                return null;

            var totalVolume = workout.Exercises.Sum(e => e.ExerciseWeight * e.ExerciseReps * e.ExerciseSets);
            var totalTime = workout.Exercises.Sum(e => e.ExerciseTime.TotalMinutes);
            var averageQuality = workout.Exercises.Any() ? workout.Exercises.Average(e => e.ExerciseQuality) : 0;

            return new WorkoutDTOs
            {
                Id = workout.Id,
                WorkoutName = workout.WorkoutName,
                WorkoutDate = workout.WorkoutDate,
                WorkoutTime = TimeSpan.FromMinutes(totalTime),
                WorkoutVolume = totalVolume,
                WorkoutQuality = (int)Math.Round(averageQuality),
                Url = $"/WorkoutDetails/{workout.WorkoutName}/{workout.Id}",
                UserId = workout.UserId // Add this line
            };
        }

        public async Task<Dictionary<string, int>> GetMonthlyWorkoutCountAsync()
        {
            // Przykładowo pobieramy treningi z ostatnich 6 miesięcy
            var workouts = await _workoutAppDbContext.Workouts
                .Where(w => w.WorkoutDate >= DateTime.Now.AddMonths(-6))
                .ToListAsync();

            // Grupujemy treningi według roku i miesiąca, formatujemy na "yyyy-MM" i liczymy elementy w każdej grupie
            var monthlyCount = workouts
                .GroupBy(w => w.WorkoutDate.ToString("yyyy-MM"))
                .ToDictionary(g => g.Key, g => g.Count());

            return monthlyCount;
        }

        // New method for user-specific workout stats
        public async Task<Dictionary<string, int>> GetMonthlyWorkoutCountForUserAsync(Guid userId)
        {
            // Pobieramy treningi z ostatnich 6 miesięcy dla konkretnego użytkownika
            var workouts = await _workoutAppDbContext.Workouts
                .Where(w => w.WorkoutDate >= DateTime.Now.AddMonths(-6) && w.UserId == userId)
                .ToListAsync();

            // Grupujemy treningi według roku i miesiąca, formatujemy na "yyyy-MM" i liczymy elementy w każdej grupie
            var monthlyCount = workouts
                .GroupBy(w => w.WorkoutDate.ToString("yyyy-MM"))
                .ToDictionary(g => g.Key, g => g.Count());

            return monthlyCount;
        }
    }
}
