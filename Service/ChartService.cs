using Client_ui.Domain.DTO;
using Client_ui.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace Client_ui.Service
{
    public class ChartService : IChartService
    {
        private readonly WorkoutAppDbContext _context;
        public ChartService(WorkoutAppDbContext workoutAppDbContext) 
        {
            _context = workoutAppDbContext;   
        }
        public async Task<IEnumerable<ChartData>> GetAllWorkoutsInMonth()
        {
            var workouts = await _context.Workouts
                .GroupBy(w => new { w.WorkoutDate.Year, w.WorkoutDate.Month})
                .Select(g => new ChartData
                {
                    Month = $"{g.Key.Year}-{g.Key.Month}",
                    Count = g.Count()
                })
                .ToListAsync();
            var result = workouts.Select(w => new ChartData
            {
                Month = w.Month,
                Count = w.Count
            }).ToList();
            return result;
        }
        public async Task<IEnumerable<ChartDataWithVolume>> GetAllWorkoutsInMonthWithVolume()
        {
            var workouts = await _context.Workouts
                .GroupBy(w => new { w.WorkoutDate.Year, w.WorkoutDate.Month })
                .Select(g => new ChartDataWithVolume
                {
                    Month = $"{g.Key.Year}-{g.Key.Month}",
                    Count = g.Count(),
                    Volume = g.Sum(w => w.Exercises.Sum(e => (decimal)e.ExerciseVolume)) 
                })
                .ToListAsync();
           var result = workouts.Select(w => new ChartDataWithVolume
            {
                Month = w.Month,
                Count = w.Count,
                Volume = w.Volume
            }).ToList();
            return result;
        }
    }
}
