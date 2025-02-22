using System.ComponentModel.DataAnnotations;

namespace BloodBank.Site.Models;

public class DonorViewModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Blood { get; set; }
}