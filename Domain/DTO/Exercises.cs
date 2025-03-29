using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_ui.Domain.DTO
{
    public class AddExercises
    {
        [Required(ErrorMessage = "Nazwa ćwiczenia jest wymagana")]
        public string ExerciseName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kategoria jest wymagana")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ciężar jest wymagany")]
        [Range(0, float.MaxValue, ErrorMessage = "Ciężar musi być większy od 0")]
        public float ExerciseWeight { get; set; }

        [Required(ErrorMessage = "Liczba powtórzeń jest wymagana")]
        [Range(1, int.MaxValue, ErrorMessage = "Liczba powtórzeń musi być większa od 0")]
        public int ExerciseReps { get; set; }

        [Required(ErrorMessage = "Liczba serii jest wymagana")]
        [Range(1, int.MaxValue, ErrorMessage = "Liczba serii musi być większa od 0")]
        public int ExerciseSets { get; set; }

        [Required(ErrorMessage = "Czas ćwiczenia jest wymagany")]
        public TimeSpan ExerciseTime { get; set; }

        [Required(ErrorMessage = "Jakość ćwiczenia jest wymagana")]
        [Range(1, 5, ErrorMessage = "Jakość ćwiczenia musi być w zakresie od 1 do 5")]
        public int ExerciseQuality { get; set; }

        public Guid WorkoutFK { get; set; }
    }

    public class EditExercises
    {
        public Guid Id { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public float ExerciseWeight { get; set; }
        public int ExerciseReps { get; set; }
        public int ExerciseSets { get; set; }
        public TimeSpan ExerciseTime { get; set; }
        public int ExerciseQuality { get; set; }
    }

    public class ExerciseDTOs
    {
        public Guid Id { get; set; }
        public Guid WorkoutId { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public float ExerciseWeight { get; set; }
        public int ExerciseReps { get; set; }
        public int ExerciseSets { get; set; }
        public TimeSpan ExerciseTime { get; set; }
        public int ExerciseQuality { get; set; }
        public float ExerciseVolume => ExerciseWeight * ExerciseReps * ExerciseSets;
    }
}
