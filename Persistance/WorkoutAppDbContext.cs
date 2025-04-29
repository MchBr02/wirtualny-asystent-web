using Client_ui.Domain;
using Microsoft.EntityFrameworkCore;
namespace Client_ui.Persistance
{
    public class WorkoutAppDbContext:DbContext
    {
        public WorkoutAppDbContext(DbContextOptions<WorkoutAppDbContext> optionsBuilder):base(optionsBuilder) { }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
