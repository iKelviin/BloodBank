namespace BloodBank.Site.Models;

public class LocalAddress
{
    public string Cep { get; set; } = string.Empty;
    public string Logradouro { get; set; } = string.Empty;
    public string Complemento { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Localidade { get; set; } = string.Empty;
    public string UF { get; set; } = string.Empty;
    public string IBGE { get; set; } = string.Empty;
    public string GIA { get; set; } = string.Empty;
    public string DDD { get; set; } = string.Empty;     
    public string SIAFI { get; set; } = string.Empty;
}