using Client_ui.Components.Enum;
using System.ComponentModel;

public enum ExerciseNameEnum
{
    // Naramienne
    [ExerciseCategory(CategoryExerciseEnum.Naramienne)]
    Wyciskanie_sztangi_sprzed_głowy,

    [ExerciseCategory(CategoryExerciseEnum.Naramienne)]
    Wyciskanie_sztangi_zza_głowy,

    [ExerciseCategory(CategoryExerciseEnum.Naramienne)]
    Wyciskanie_hantli_siedząc,

    [ExerciseCategory(CategoryExerciseEnum.Naramienne)]
    Wyciskanie_hantli_na_skosie_dodatnim,

    [ExerciseCategory(CategoryExerciseEnum.Naramienne)]
    Unoszenie_hantli_bokiem,


    // Piersiowe
    [ExerciseCategory(CategoryExerciseEnum.Piersiowe)]
    Wyciskanie_sztangi_na_ławce_poziomej,

    [ExerciseCategory(CategoryExerciseEnum.Piersiowe)]
    Wyciskanie_sztangielek_na_ławce_poziomej,

    [ExerciseCategory(CategoryExerciseEnum.Piersiowe)]
    Wyciskanie_sztangi_na_ławce_skosnej_głową_w_górę,

    [ExerciseCategory(CategoryExerciseEnum.Piersiowe)]
    Wyciskanie_sztangielek_na_ławce_skosnej_głową_w_górę,

    [ExerciseCategory(CategoryExerciseEnum.Piersiowe)]
    Rozpiętki_na_ławce_poziomej,

    [ExerciseCategory(CategoryExerciseEnum.Piersiowe)]
    Rozpiętki_na_ławce_skosnej_głową_w_górę,


    // Grzbietu
    [ExerciseCategory(CategoryExerciseEnum.Grzbietu)]
    Podciąganie_na_drążku_szerokim_uchwycie,

    [ExerciseCategory(CategoryExerciseEnum.Grzbietu)]
    Podciąganie_na_drążku_wąskim_uchwycie,

    [ExerciseCategory(CategoryExerciseEnum.Grzbietu)]
    Wiosłowanie_sztangą_w_opadzie,

    [ExerciseCategory(CategoryExerciseEnum.Grzbietu)]
    Wiosłowanie_hantlami_w_podporze,

    [ExerciseCategory(CategoryExerciseEnum.Grzbietu)]
    Wiosłowanie_sztangielkami_w_opadzie,


    // Nogi
    [ExerciseCategory(CategoryExerciseEnum.Nogi)]
    Przysiady,

    [ExerciseCategory(CategoryExerciseEnum.Nogi)]
    Wykroki,

    [ExerciseCategory(CategoryExerciseEnum.Nogi)]
    Prostowanie_nóg_na_maszynie,

    [ExerciseCategory(CategoryExerciseEnum.Nogi)]
    Uginanie_nóg_na_maszynie,

    [ExerciseCategory(CategoryExerciseEnum.Nogi)]
    Wspięcia_na_palce_stojąc,


    // Brzuch
    [ExerciseCategory(CategoryExerciseEnum.Brzuch)]
    Skłony_na_ławce_skosnej,

    [ExerciseCategory(CategoryExerciseEnum.Brzuch)]
    Skłony_na_ławce_poziomej,

    [ExerciseCategory(CategoryExerciseEnum.Brzuch)]
    Skłony_na_ławce_skosnej_z_obrótami,

    [ExerciseCategory(CategoryExerciseEnum.Brzuch)]
    Skłony_na_ławce_poziomej_z_obrótami,

    [ExerciseCategory(CategoryExerciseEnum.Brzuch)]
    Skłony_na_ławce_skosnej_z_obciążeniem,

    [ExerciseCategory(CategoryExerciseEnum.Brzuch)]
    Skłony_na_ławce_poziomej_z_obciążeniem,

    [ExerciseCategory(CategoryExerciseEnum.Brzuch)]
    Skłony_na_ławce_skosnej_z_obrótami_i_obciążeniem,

    [ExerciseCategory(CategoryExerciseEnum.Brzuch)]
    Skłony_na_ławce_poziomej_z_obrótami_i_obciążeniem,
}
