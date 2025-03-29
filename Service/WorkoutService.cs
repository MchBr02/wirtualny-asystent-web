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
                WorkoutDate = addWorkoutDTO.WorkoutDate
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
                workout.WorkoutName = updateWorkoutDTO.WorkoutName;
                workout.WorkoutDate = updateWorkoutDTO.WorkoutDate;
            }
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
                Url = $"/WorkoutDetails/{workout.WorkoutName}/{workout.Id}"
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


    }
}
