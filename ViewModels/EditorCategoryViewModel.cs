using System.ComponentModel.DataAnnotations;

namespace TF.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "O nome � obrigat�rio")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo deve conter entre 3 e 40 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O slug � obrigat�rio")]
        public string Slug { get; set; }
    }
}