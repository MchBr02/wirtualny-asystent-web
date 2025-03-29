using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_ui.Domain
{
    public class Exercise
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Workout")]
        public Guid WorkoutId { get; set; }
        public Workout? Workout { get; set; }
        [Required]
        public string ExerciseName { get; set; } = default!;
        public int ExerciseQuality { get; set; }
        public float ExerciseWeight { get; set; }
        public int ExerciseReps { get; set; }
        public int ExerciseSets { get; set; }
        public float ExerciseVolume { get; set; }
        [Required]
        public string Category { get; set; } = default!;
        public TimeSpan ExerciseTime { get; set; }
    }
}
