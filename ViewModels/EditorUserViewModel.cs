using System.ComponentModel.DataAnnotations;

namespace TF.ViewModels
{
    public class EditorUserViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo deve conter entre 3 e 40 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve conter entre 6 e 20 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A imagem é obrigatória")]
        [Url(ErrorMessage = "A imagem deve ser uma URL válida")]
        public string Image { get; set; } = string.Empty;

        [Required(ErrorMessage = "O slug é obrigatório")]
        public string Slug { get; set; } = string.Empty;
    }
}