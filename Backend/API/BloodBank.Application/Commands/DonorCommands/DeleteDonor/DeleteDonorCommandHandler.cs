using BloodBank.Application.Models;
using BloodBank.Core.Interfaces;
using MediatR;

namespace BloodBank.Application.Commands.DonorCommands.DeleteDonor;

public class DeleteDonorCommandHandler : IRequestHandler<DeleteDonorCommand, ResultViewModel>
{
    private readonly IDonorRepository _repository;

    public DeleteDonorCommandHandler(IDonorRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(DeleteDonorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var donor = await _repository.GetById(request.Id);
            if (donor is null) return ResultViewModel.Error("Donor not found");

            donor.SetAsDeleted();
            await _repository.Update(donor);
            return ResultViewModel.Success();
        }
        catch (Exception e)
        {
            return ResultViewModel.Error($"Error occured: {e.Message}");
        }
    }
}