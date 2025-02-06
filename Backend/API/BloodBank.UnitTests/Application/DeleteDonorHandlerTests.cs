using BloodBank.Application.Commands.DonorCommands.DeleteDonor;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.Interfaces;
using NSubstitute;

namespace BloodBank.UnitTests.Application;

public class DeleteDonorHandlerTests
{
    private readonly IDonorRepository _repository;
    private readonly DeleteDonorCommandHandler _handler;

    public DeleteDonorHandlerTests()
    {
        _repository = Substitute.For<IDonorRepository>();
        _handler = new DeleteDonorCommandHandler(_repository);
    }
    
    [Fact]
    public async Task Handle_WhenDonorExists_MarksDonorAsDeleted()
    {
        // Arrange
        var donorId = Guid.NewGuid();
        var donor = new Donor("John Doe", "john@example.com", new DateTime(1990, 1, 1), GenderEnum.Male, 70, BloodTypeEnum.A, RhFactorEnum.Positive, new Address("Street","City","State","12345-678"));
        var command = new DeleteDonorCommand(donorId);

        _repository.GetById(donorId).Returns(donor);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await _repository.Received(1).Update(donor);
    }

    [Fact]
    public async Task Handle_WhenDonorDoesNotExist_ReturnsError()
    {
        // Arrange
        var donorId = Guid.NewGuid();
        var command = new DeleteDonorCommand(donorId);

        _repository.GetById(donorId).Returns((Donor)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Donor not found", result.Message);
    }
}