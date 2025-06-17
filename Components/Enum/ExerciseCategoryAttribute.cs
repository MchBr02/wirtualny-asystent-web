using System;

namespace Client_ui.Components.Enum
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ExerciseCategoryAttribute : Attribute
    {
        public CategoryExerciseEnum Category { get; }
        public ExerciseCategoryAttribute(CategoryExerciseEnum category)
            => Category = category;
    }
}
