using BloodBank.Core.Entities;
using BloodBank.Core.Enums;

namespace BloodBank.UnitTests.Core;

public class DonorTests
{
    [Fact]
    public void DonorCanBeDonated_Return_True()
    {
        //Arrange
        var donor = new Donor("Doador Teste", "doador@mail.com", DateTime.Now.AddYears(-18), GenderEnum.Male, 70, "A",
            "Positive", new Address("Rua teste", "Cidade teste", "SP", "06735-075"));

        //Act
        var result = donor.CanDonate();
        
        //Assert
        Assert.True(result);
    }
    
    [Fact]
    public void DonorCannotBeDonated_Return_False()
    {
        //Arrange
        var donor = new Donor("Doador Teste", "doador@mail.com", DateTime.Now.AddYears(-15), GenderEnum.Male, 49, "A",
            "Positive", new Address("Rua teste", "Cidade teste", "SP", "06735-075"));

        //Act
        var result = donor.CanDonate();
        
        //Assert
        Assert.False(result);
    }
}