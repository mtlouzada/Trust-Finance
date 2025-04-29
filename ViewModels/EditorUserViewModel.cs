using System.ComponentModel.DataAnnotations;

namespace TF.ViewModels
{
    public class EditorUserViewModel
    {
        [Required(ErrorMessage = "O nome � obrigat�rio")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo deve conter entre 3 e 40 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O email � obrigat�rio")]
        [EmailAddress(ErrorMessage = "Email inv�lido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha � obrigat�ria")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve conter entre 6 e 20 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A imagem � obrigat�ria")]
        [Url(ErrorMessage = "A imagem deve ser uma URL v�lida")]
        public string Image { get; set; } = string.Empty;

        [Required(ErrorMessage = "O slug � obrigat�rio")]
        public string Slug { get; set; } = string.Empty;
    }
}