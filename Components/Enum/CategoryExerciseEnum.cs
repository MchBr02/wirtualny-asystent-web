using System.ComponentModel.DataAnnotations;

namespace Client_ui.Components.Enum
{
    public enum CategoryExerciseEnum
    {
        [Display(Name = "Rozciąganie")]
        Rozciaganie,

        [Display(Name = "Naramienne")]
        Naramienne,

        [Display(Name = "Piersiowe")]
        Piersiowe,

        [Display(Name = "Grzbietu")]
        Grzbietu,

        [Display(Name = "Nogi")]
        Nogi,

        [Display(Name = "Pośladki")]
        Posladki,

        [Display(Name = "Brzuch")]
        Brzuch,
    }
}
