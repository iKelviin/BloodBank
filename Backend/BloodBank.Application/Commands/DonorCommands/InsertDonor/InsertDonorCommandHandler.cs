using BloodBank.Application.Models;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Commands.DonorCommands.InsertDonor;

public class InsertDonorCommandHandler : IRequestHandler<InsertDonorCommand, ResultViewModel<Guid>>
{
    private readonly IDonorRepository _repository;

    public InsertDonorCommandHandler(IDonorRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<Guid>> Handle(InsertDonorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existsDonor = await _repository.Exists(request.Email);
            if (existsDonor) return ResultViewModel<Guid>.Error("Donor with this e-mail already exists");
            
            var donor = request.ToEntity();
            await _repository.Add(donor);
            return ResultViewModel<Guid>.Success(donor.Id);
        }
        catch (Exception e)
        {
            return ResultViewModel<Guid>.Error($"Error occured: {e.Message}");
        }
    }
}