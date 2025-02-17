using BloodBank.Application.Commands.DonationCommands.InsertDonation;
using BloodBank.Application.Validators;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.Interfaces;
using FluentValidation.TestHelper;
using NSubstitute;

namespace BloodBank.UnitTests.Application;

public class InsertDonationHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly InsertDonationCommandHandler _handler;
    private readonly IDonorRepository _donorRepository;
    private readonly IDonationRepository _donationRepository;
    private readonly IStockRepository _stockRepository;
    private readonly InsertDonationValidator _validator;

    
    public InsertDonationHandlerTests()
    {
        _validator = new InsertDonationValidator();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _donorRepository = Substitute.For<IDonorRepository>();
        _donationRepository = Substitute.For<IDonationRepository>();
        _stockRepository = Substitute.For<IStockRepository>();
        
        _unitOfWork.Donors.Returns(_donorRepository);
        _unitOfWork.Donations.Returns(_donationRepository);
        _unitOfWork.Stocks.Returns(_stockRepository);
        
        _handler = new InsertDonationCommandHandler(_unitOfWork);
    }

    [Fact]
    public async Task Handle_WhenDonorNotFound_RetunsError()
    {
        // Arrange
        var donorId = Guid.NewGuid();
        var command = new InsertDonationCommand(donorId,Guid.NewGuid(), DateTime.Now,450);
        
        _donorRepository.GetById(donorId).Returns((Donor)null);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Donor not found", result.Message);
    }

    [Fact]
    public async Task Handle_WhenDonorCannotDonate_RetunsError()
    {
        // Arrange
        var donorId = Guid.NewGuid();
        var donor = new Donor("Lucas Silva", "lucas@mail.com", new DateTime(2020, 6, 3), GenderEnum.Male, 40, BloodTypeEnum.A,
            RhFactorEnum.Positive, new Address("Street", "City", "State", "12345-678"),"password","role");

        var command = new InsertDonationCommand(donorId,Guid.NewGuid(), DateTime.Now, 420);
        _donorRepository.GetById(donorId).Returns(donor);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Donor can't donate", result.Message);
        Assert.False(donor.CanDonate());
    }

    [Fact]
    public async Task Handle_WhenDonorCanDonate_RetunsSuccess()
    {
        // Arrange
        var donorId = Guid.NewGuid();
        var donor = new Donor("Lucas Silva", "lucas@mail.com", new DateTime(2000, 6, 3), GenderEnum.Male, 70,BloodTypeEnum.A,
            RhFactorEnum.Positive, new Address("Street", "City", "State", "12345-678"),"password","role");
        
        var command = new InsertDonationCommand(donorId,Guid.NewGuid(), DateTime.Now, 450);
        _donorRepository.GetById(donorId).Returns(donor);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(donor.CanDonate());
    }

    [Fact]
    public async Task Handle_WhenMaleDonorDonatesBefore60Days_ReturnsError()
    {
        // Arrange
        var donorId = Guid.NewGuid();
        var donor = new Donor("Lucas Silva", "lucas@mail.com", new DateTime(2000, 6, 3), GenderEnum.Male, 70, BloodTypeEnum.A,
            RhFactorEnum.Positive, new Address("Street", "City", "State", "12345-678"),"password","role");

        var lastDonation = new Donation(donorId,Guid.NewGuid(), DateTime.Now.AddDays(-50), 450);
        var command = new InsertDonationCommand(donorId,Guid.NewGuid(), DateTime.Now, 450);
        
        _donorRepository.GetById(donorId).Returns(donor);
        _donationRepository.GetLastByDonorId(donorId).Returns(lastDonation);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Men can only donate blood every 60 days.", result.Message);
    }
    
    [Fact]
    public async Task Handle_WhenFemaleDonorDonatesBefore90Days_ReturnsError()
    {
        // Arrange
        var donorId = Guid.NewGuid();
        var donor = new Donor("Lucia Silva", "lucia@mail.com", new DateTime(2000, 6, 3), GenderEnum.Female, 70, BloodTypeEnum.B,
            RhFactorEnum.Negative, new Address("Street", "City", "State", "12345-678"),"password","role");

        var lastDonation = new Donation(donorId,Guid.NewGuid(), DateTime.Now.AddDays(-80), 450);
        var command = new InsertDonationCommand(donorId,Guid.NewGuid(), DateTime.Now, 450);
        
        _donorRepository.GetById(donorId).Returns(donor);
        _donationRepository.GetLastByDonorId(donorId).Returns(lastDonation);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Women can only donate blood every 90 days.", result.Message);
    }

    [Fact]
    public async Task Handle_WhenDonationIsValid_AddsDonationAndUpdateStock()
    {
        // Arrange
        var donorId = Guid.NewGuid();
        var donor = new Donor("Lucas Silva", "lucas@mail.com", new DateTime(2000, 6, 3), GenderEnum.Male, 70, BloodTypeEnum.A,
            RhFactorEnum.Positive, new Address("Street", "City", "State", "12345-678"),"password","role");
        
        var command = new InsertDonationCommand(donorId,Guid.NewGuid(), DateTime.Now, 450);
        
        _donorRepository.GetById(donorId).Returns(donor);
        _donationRepository.GetLastByDonorId(donorId).Returns((Donation)null);
        _stockRepository.GetByBloodType(Arg.Any<string>(), Arg.Any<string>()).Returns((Stock)null);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        await _donationRepository.Received(1).Add(Arg.Any<Donation>());
        await _stockRepository.Received(1).Add(Arg.Any<Stock>());
        await _unitOfWork.Received(1).CommitAsync();
    }

    [Fact]
    public void Validate_WhenQuantityMlIsLessThan420_ShouldHaveValidationError()
    {
        // Arrange
        var command = new InsertDonationCommand(Guid.NewGuid(),Guid.NewGuid(), DateTime.Now, 400);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.QuantityMl)
            .WithErrorMessage("Quantity blood must be between 420ml and 470ml.");
    }
    
    [Fact]
    public void Validate_WhenQuantityMlIsGreaterThan470_ShouldHaveValidationError()
    {
        // Arrange
        var command = new InsertDonationCommand(Guid.NewGuid(),Guid.NewGuid(), DateTime.Now, 500);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.QuantityMl)
            .WithErrorMessage("Quantity blood must be between 420ml and 470ml.");
    }
}