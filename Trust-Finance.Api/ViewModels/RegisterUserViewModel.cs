using System.ComponentModel.DataAnnotations;

namespace TF.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo deve conter entre 3 e 40 caracteres")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve conter entre 6 e 20 caracteres")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "A imagem é obrigatória")]
        [Url(ErrorMessage = "A imagem deve ser uma URL válida")]
        public required string Image { get; set; }

        [Required(ErrorMessage = "O slug é obrigatório")]
        public required string Slug { get; set; }
    }
}
