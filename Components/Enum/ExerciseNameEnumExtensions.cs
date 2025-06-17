using System;
using System.Reflection;

namespace Client_ui.Components.Enum
{
    public static class ExerciseNameEnumExtensions
    {
        public static CategoryExerciseEnum GetCategory(this ExerciseNameEnum value)
        {
            // Odczyt atrybutu z pola enuma
            var fi = value.GetType().GetField(value.ToString())
                     ?? throw new InvalidOperationException($"Pole {value} nie istnieje");
            var attr = fi.GetCustomAttribute<ExerciseCategoryAttribute>();
            if (attr is null)
                throw new InvalidOperationException($"Brak atrybutu ExerciseCategoryAttribute dla {value}");
            return attr.Category;
        }

        public static string ToDisplayString(this ExerciseNameEnum value)
            => value.ToString().Replace('_', ' ');
    }
}
