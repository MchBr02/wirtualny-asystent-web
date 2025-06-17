using Client_ui.Components.Enum;
using Client_ui.Domain;
using Client_ui.Domain.DTO;
using Client_ui.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Client_ui.Service
{
    public class ExerciseService : IExerciseService
    {
        private readonly WorkoutAppDbContext _workoutAppDbContext;

        public ExerciseService(WorkoutAppDbContext workoutAppDbContext)
        {
            _workoutAppDbContext = workoutAppDbContext;
        }

        public async Task<ExerciseDTOs> GetExercise(Guid id)
        {
            var exercise = await _workoutAppDbContext.Exercises
                .Include(e => e.Workout)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exercise == null)
            {
                throw new KeyNotFoundException("Ćwiczenie nie zostało znalezione");
            }

            return new ExerciseDTOs
            {
                Id = exercise.Id,
                WorkoutId = exercise.WorkoutId,
                ExerciseName = exercise.ExerciseName,
                ExerciseWeight = exercise.ExerciseWeight,
                ExerciseReps = exercise.ExerciseReps,
                ExerciseSets = exercise.ExerciseSets,
                Category = exercise.Category,
                ExerciseTime = exercise.ExerciseTime,
                ExerciseQuality = exercise.ExerciseQuality
            };
        }

        public async Task<IEnumerable<ExerciseDTOs>> GetExercisesForWorkout(Guid workoutId)
        {
            var exercises = await _workoutAppDbContext.Exercises
                .Where(x => x.WorkoutId == workoutId)
                .Select(x => new ExerciseDTOs
                {
                    Id = x.Id,
                    WorkoutId = x.WorkoutId,
                    ExerciseName = x.ExerciseName,
                    ExerciseWeight = x.ExerciseWeight,
                    ExerciseReps = x.ExerciseReps,
                    ExerciseSets = x.ExerciseSets,
                    Category = x.Category,
                    ExerciseTime = x.ExerciseTime,
                    ExerciseQuality = x.ExerciseQuality
                })
                .ToListAsync();

            return exercises;
        }

        public async Task<ExerciseDTOs> CreateExercise(AddExercises exercise)
        {
            var workout = await _workoutAppDbContext.Workouts.FindAsync(exercise.WorkoutFK);
            if (workout == null)
            {
                throw new KeyNotFoundException("Trening nie został znaleziony");
            }

            var newExercise = new Exercise
            {
                Id = Guid.NewGuid(),
                WorkoutId = workout.Id,
                ExerciseName = exercise.ExerciseName,
                ExerciseWeight = exercise.ExerciseWeight,
                ExerciseReps = exercise.ExerciseReps,
                ExerciseSets = exercise.ExerciseSets,
                Category = exercise.Category,
                ExerciseTime = exercise.ExerciseTime,
                ExerciseQuality = exercise.ExerciseQuality,
                ExerciseVolume = exercise.ExerciseWeight * exercise.ExerciseReps * exercise.ExerciseSets
            };

            _workoutAppDbContext.Exercises.Add(newExercise);
            await _workoutAppDbContext.SaveChangesAsync();

            return new ExerciseDTOs
            {
                Id = newExercise.Id,
                WorkoutId = newExercise.WorkoutId,
                ExerciseName = newExercise.ExerciseName,
                ExerciseWeight = newExercise.ExerciseWeight,
                ExerciseReps = newExercise.ExerciseReps,
                ExerciseSets = newExercise.ExerciseSets,
                Category = newExercise.Category,
                ExerciseTime = newExercise.ExerciseTime,
                ExerciseQuality = newExercise.ExerciseQuality
            };
        }

        public async Task<ExerciseDTOs> UpdateExercise(EditExercises exercise)
        {
            var existingExercise = await _workoutAppDbContext.Exercises.FindAsync(exercise.Id);
            if (existingExercise == null)
            {
                throw new KeyNotFoundException("Ćwiczenie nie zostało znalezione");
            }

            existingExercise.ExerciseName = exercise.ExerciseName;
            existingExercise.Category = exercise.Category;
            existingExercise.ExerciseWeight = exercise.ExerciseWeight;
            existingExercise.ExerciseReps = exercise.ExerciseReps;
            existingExercise.ExerciseSets = exercise.ExerciseSets;
            existingExercise.ExerciseTime = exercise.ExerciseTime;
            existingExercise.ExerciseQuality = exercise.ExerciseQuality;
            existingExercise.ExerciseVolume = exercise.ExerciseWeight * exercise.ExerciseReps * exercise.ExerciseSets;

            await _workoutAppDbContext.SaveChangesAsync();

            return new ExerciseDTOs
            {
                Id = existingExercise.Id,
                WorkoutId = existingExercise.WorkoutId,
                ExerciseName = existingExercise.ExerciseName,
                ExerciseWeight = existingExercise.ExerciseWeight,
                ExerciseReps = existingExercise.ExerciseReps,
                ExerciseSets = existingExercise.ExerciseSets,
                Category = existingExercise.Category,
                ExerciseTime = existingExercise.ExerciseTime,
                ExerciseQuality = existingExercise.ExerciseQuality
            };
        }

        public async Task<IEnumerable<ExerciseDTOs>> GetAllExercisesByWorkoutId(Guid workoutId)
        {
            var exercises = await _workoutAppDbContext.Exercises
                .Where(x => x.WorkoutId == workoutId)
                .Select(x => new ExerciseDTOs
                {
                    Id = x.Id,
                    WorkoutId = x.WorkoutId,
                    ExerciseName = x.ExerciseName,
                    ExerciseWeight = x.ExerciseWeight,
                    ExerciseReps = x.ExerciseReps,
                    ExerciseSets = x.ExerciseSets,
                    Category = x.Category,
                    ExerciseTime = x.ExerciseTime,
                    ExerciseQuality = x.ExerciseQuality
                })
                .ToListAsync();

            return exercises;
        }

        public async Task<float> GetWorkoutVolume(Guid workoutId)
        {
            var exercises = await _workoutAppDbContext.Exercises
                .Where(x => x.WorkoutId == workoutId)
                .ToListAsync();

            return exercises.Sum(x => x.ExerciseWeight * x.ExerciseReps * x.ExerciseSets);
        }

        public async Task<TimeSpan> GetTotalExerciseTime(Guid workoutId)
        {
            var exercises = await _workoutAppDbContext.Exercises
                .Where(x => x.WorkoutId == workoutId)
                .ToListAsync();

            return exercises.Aggregate(TimeSpan.Zero, (acc, x) => acc + x.ExerciseTime);
        }

        public async Task DeleteExercise(Guid id)
        {
            var exercise = await _workoutAppDbContext.Exercises.FindAsync(id);
            if (exercise == null)
            {
                throw new KeyNotFoundException("Exercise Not Found");
            }
            _workoutAppDbContext.Exercises.Remove(exercise);
            await _workoutAppDbContext.SaveChangesAsync();
        }

        public IEnumerable<string> GetExercisesForCategory(string category)
        {
            if (!Enum.TryParse<CategoryExerciseEnum>(category, true, out var catEnum))
                return Enumerable.Empty<string>();

            return Enum.GetValues<ExerciseNameEnum>()
                       .Cast<ExerciseNameEnum>()
                       .Where(e => e.GetCategory() == catEnum)
                       .Select(e => e.ToDisplayString());
        }


        public async Task<IEnumerable<ExerciseDTOs>> GetAllExercises()
        {
            var exercises = await _workoutAppDbContext.Exercises
                .Select(x => new ExerciseDTOs
                {
                    Id = x.Id,
                    WorkoutId = x.WorkoutId,
                    ExerciseName = x.ExerciseName,
                    ExerciseWeight = x.ExerciseWeight,
                    ExerciseReps = x.ExerciseReps,
                    ExerciseSets = x.ExerciseSets,
                    Category = x.Category,
                    ExerciseTime = x.ExerciseTime,
                    ExerciseQuality = x.ExerciseQuality
                })
                .ToListAsync();

            return exercises;
        }

        public async Task<Dictionary<string, float>> GetExerciseProgress(string exerciseName)
        {
            try
            {
                var exercises = await _workoutAppDbContext.Exercises
                    .Include(e => e.Workout)
                    .Where(e => e.ExerciseName == exerciseName)
                    .OrderBy(e => e.Workout.WorkoutDate)
                    .Select(x => new { x.Workout.WorkoutDate, x.ExerciseWeight })
                    .ToListAsync();

                return exercises.ToDictionary(
                    x => x.WorkoutDate.ToString("yyyy-MM-dd"),
                    x => x.ExerciseWeight
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania postępu ćwiczenia: {ex.Message}");
                return new Dictionary<string, float>();
            }
        }

        /*public async Task<Dictionary<string, float>> GetCategoryVolumeDistribution()
        {
            try
            {
                var exercises = await _workoutAppDbContext.Exercises
                    .Include(e => e.Workout)
                    .Where(e => e.Workout.WorkoutDate >= DateTime.Now.AddMonths(-1))
                    .ToListAsync();

                Console.WriteLine("\nWszystkie ćwiczenia z ostatniego miesiąca:");
                foreach (var exercise in exercises)
                {
                    Console.WriteLine($"ID: {exercise.Id}, Kategoria: {exercise.Category}, Nazwa: {exercise.ExerciseName}, " +
                                    $"Ciężar: {exercise.ExerciseWeight}, Powtórzenia: {exercise.ExerciseReps}, Serie: {exercise.ExerciseSets}, " +
                                    $"Data treningu: {exercise.Workout.WorkoutDate:yyyy-MM-dd}");
                }

                var volumeDistribution = exercises
                    .GroupBy(e => e.Category)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Sum(e => e.ExerciseWeight * e.ExerciseReps * e.ExerciseSets)
                    );

                Console.WriteLine("\nRozkład objętości według kategorii:");
                foreach (var category in volumeDistribution)
                {
                    Console.WriteLine($"Kategoria: {category.Key}, Objętość: {category.Value}");
                }

                return volumeDistribution;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania rozkładu objętości: {ex.Message}");
                return new Dictionary<string, float>();
            }
        }*/

        public async Task<Dictionary<string, int>> GetExerciseFrequency()
        {
            var exercises = await _workoutAppDbContext.Exercises
                .ToListAsync();

            var frequency = exercises
                .GroupBy(e => e.ExerciseName)
                .OrderByDescending(g => g.Count())
                .Take(10)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count()
                );

            return frequency;
        }

        /*public async Task<Dictionary<string, int>> GetMonthlyWorkoutCount()
        {
            try
            {
                var workouts = await _workoutAppDbContext.Workouts
                    .Where(w => w.WorkoutDate >= DateTime.Now.AddMonths(-6))
                    .OrderBy(w => w.WorkoutDate)
                    .ToListAsync();

                Console.WriteLine("Wszystkie treningi:");
                foreach (var workout in workouts)
                {
                    Console.WriteLine($"ID: {workout.Id}, Data: {workout.WorkoutDate:yyyy-MM-dd}");
                }

                var monthlyCount = workouts
                    .GroupBy(w => w.WorkoutDate.ToString("yyyy-MM"))
                    .OrderBy(g => g.Key)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Count()
                    );

                Console.WriteLine("\nLiczba treningów w miesiącach:");
                foreach (var month in monthlyCount)
                {
                    Console.WriteLine($"Miesiąc: {month.Key}, Liczba treningów: {month.Value}");
                }

                return monthlyCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania liczby treningów: {ex.Message}");
                return new Dictionary<string, int>();
            }
        }*/

        public async Task<Dictionary<string, float>> GetExerciseVolumeProgress(string exerciseName)
        {
            var exercises = await _workoutAppDbContext.Exercises
                .Include(e => e.Workout)
                .Where(e => e.ExerciseName == exerciseName)
                .OrderBy(e => e.Workout.WorkoutDate)
                .ToListAsync();

            var volumeProgress = exercises
                .GroupBy(e => e.Workout.WorkoutDate.ToString("yyyy-MM-dd"))
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(e => e.ExerciseWeight * e.ExerciseReps * e.ExerciseSets)
                );

            return volumeProgress;
        }

        

    }
}
