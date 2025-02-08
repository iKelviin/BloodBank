using System.ComponentModel.DataAnnotations;

namespace BloodBank.Site.Models;

public class DonorViewModel
{
    public Guid Id { get;  set; }
    
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string FullName { get;  set; }
    
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress]
    public string Email { get;  set; }
    
    [Required(ErrorMessage = "A idade é obrigatória.")]
    public int Age { get;  set; }
    
    [Required(ErrorMessage = "O sexo é obrigatório.")]
    public string Gender { get;  set; }
    
    [Required(ErrorMessage = "O tipo sanguineo é obrigatório.")]
    public string Blood { get;  set; }
}