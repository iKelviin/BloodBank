using BloodBank.Application.Commands.DonorCommands.InsertDonor;
using BloodBank.Application.Validators;
using BloodBank.Core.Entities;
using BloodBank.Core.Interfaces;
using BloodBank.Infrastructure.Security;
using FluentValidation.TestHelper;
using NSubstitute;

namespace BloodBank.UnitTests.Application;

public class InsertDonorHandlerTests
{
    private readonly IDonorRepository _repository;
    private readonly InsertDonorCommandHandler _handler;
    private readonly IAuthService _authService;
    private readonly RegisterDonorValidator _validator;

    public InsertDonorHandlerTests()
    {
        _repository = Substitute.For<IDonorRepository>();
        _authService = Substitute.For<IAuthService>();
        _handler = new InsertDonorCommandHandler(_repository,_authService);
        _validator = new RegisterDonorValidator();
    }

    [Fact]
    public async Task Handle_WhenEmailExists_ReturnsError()
    {
        // Arrange
        var email = "john@example.com";
        var command = new InsertDonorCommand(
            "John Doe", email, new DateTime(1990, 1, 1), "Male", 70, "A", "Positive", "Street", "City", "State", "12345-678");

        _repository.Exists(email).Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Donor with this e-mail already exists", result.Message);
    }
    
    [Fact]
    public async Task Handle_WhenDonorIsValid_AddsDonor()
    {
        // Arrange
        var email = "john@example.com";
        var command = new InsertDonorCommand(
            "John Doe", email, new DateTime(1990, 1, 1), "Male", 70, "A", "Positive", "Street", "City", "State", "12345-678");

        _repository.Exists(email).Returns(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await _repository.Received(1).Add(Arg.Any<Donor>());
    }
}