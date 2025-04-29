using System.ComponentModel.DataAnnotations;

namespace TF.ViewModels;
{   
    public class EditorTransactionViewModel
    {
        [Required(ErrorMessage = "Campo obrigat�rio")]
        [StringLength(100, ErrorMessage = "O campo deve ter entre 3 e 100 caracteres", MinimumLength = 3)]
        
    public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo obrigat�rio")]
        [Range(0.01, 9999999999.99, ErrorMessage = "Valor inv�lido")]
        
    public decimal Amount { get; set; }
        [Required(ErrorMessage = "Campo obrigat�rio")]
        
    public DateTime Date { get; set; }
        [Required(ErrorMessage = "Campo obrigat�rio")]
        
    public int CategoryId { get; set; }
    }
}