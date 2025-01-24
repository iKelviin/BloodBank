using BloodBank.Application.Models;
using MediatR;

namespace BloodBank.Application.Commands.DonorCommands.DeleteDonor;

public class DeleteDonorCommand : IRequest<ResultViewModel>
{
    public DeleteDonorCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}