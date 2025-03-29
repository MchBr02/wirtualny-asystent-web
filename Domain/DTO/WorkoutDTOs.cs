using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Client_ui.Domain.DTO.AddExercises;

namespace Client_ui.Domain.DTO
{
    public class AddWorkoutDTO
    {
        [Required]
        public string WorkoutName { get; set; } = default!;
        public int WorkoutQuality { get; set; }
        public TimeSpan WorkoutTime { get; set; }
        public DateTime WorkoutDate { get; set; }
    }
    public class UpdateWorkoutDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string WorkoutName { get; set; } = default!;
        public int WorkoutQuality { get; set; }
        public TimeSpan WorkoutTime { get; set; }
        public DateTime WorkoutDate { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class WorkoutDTOs
    {
        public Guid Id { get; set; }
        [Required]
        public string WorkoutName { get; set; } = default!;
        public int WorkoutQuality { get; set; }
        public float WorkoutVolume
        {
            get
            {
                float volume = 0;
                foreach (var exercise in Exercises)
                {
                    volume += exercise.ExerciseVolume;
                }
                Console.WriteLine(volume);
                return volume;
            }
            set { }
        }
        public ICollection<ExerciseDTOs> Exercises { get; set; } = new List<ExerciseDTOs>();
        public TimeSpan WorkoutTime { get; set; }

        public DateTime WorkoutDate { get; set; }
        [Required]
        public string Url { get; set; } = default!;
    }
}
