using BloodBank.Application.Models;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Commands.DonorCommands.UpdateDonor;

public class UpdateDonorCommandHandler : IRequestHandler<UpdateDonorCommand, ResultViewModel>
{
    private readonly IDonorRepository _repository;

    public UpdateDonorCommandHandler(IDonorRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(UpdateDonorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var donor = await _repository.GetById(request.Id);
            if (donor is null) return ResultViewModel.Error("Donor not found");

            donor.Update(
                request.FullName,
                request.Email,
                request.BirthDay,
                Enum.Parse<GenderEnum>(request.Gender),
                request.Weight,
                Enum.Parse<BloodTypeEnum>(request.BloodType),
                Enum.Parse<RhFactorEnum>(request.RhFactor),
                new Address(
                    request.Street,
                    request.City,
                    request.State,
                    request.ZipCode)
            );
            
            await _repository.Update(donor);
            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error($"Error occured: {e.Message}");
        }
    }
}