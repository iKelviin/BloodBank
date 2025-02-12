using System.ComponentModel.DataAnnotations;

namespace BloodBank.Site.Models;

public class DonorDetailsViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Data de nascimento é obrigatória")]
    public DateTime BirthDay { get; set; }

    public int Age { get; set; }

    [Required(ErrorMessage = "O sexo é obrigatório.")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "O peso é obrigatório.")]
    public Double Weight { get; set; }

    public string Blood { get; set; }
    
    [Required(ErrorMessage = "O tipo sanguineo é obrigatório.")]
    public string BloodType { get; set; }

    [Required(ErrorMessage = "O fator Rh é obrigatório.")]
    public string RhFactor { get; set; }

    [Required(ErrorMessage = "O endereço é obrigatório.")]
    public string Street { get; set; }

    [Required(ErrorMessage = "A cidade é obrigatória.")]
    public string City { get; set; }

    [Required(ErrorMessage = "A UF é obrigatória.")]
    public string State { get; set; }

    [Required(ErrorMessage = "O CEP é obrigatório.")]
    public string ZipCode { get; set; }

    public string LastDonation { get; set; }
}