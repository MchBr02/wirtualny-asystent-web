using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Client_ui.Domain.DTO;
using static Client_ui.Domain.DTO.ExerciseDTOs;
namespace Client_ui.Domain

{
    public class Workout
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string WorkoutName { get; set; } = default!;
        public int WorkoutQuality { get; set; }
        public float WorkoutVolume { get; set; }
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
        public TimeSpan WorkoutTime { get; set; }
        public DateTime WorkoutDate { get; set; }
    }
}
