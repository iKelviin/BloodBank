using BloodBank.Application.Models;
using BloodBank.Core.Interfaces;
using BloodBank.Infrastructure.Security;
using MediatR;

namespace BloodBank.Application.Commands.AuthCommands.RegisterCommand;

public class RegisterDonorCommandHandler : IRequestHandler<RegisterDonorCommand, ResultViewModel<Guid>>
{
    private readonly IDonorRepository _repository;
    private readonly IAuthService _authService;
    
    public RegisterDonorCommandHandler(IDonorRepository repository, IAuthService authService)
    {
        _repository = repository;
        _authService = authService;
    }
    
    public async Task<ResultViewModel<Guid>> Handle(RegisterDonorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existsDonor = await _repository.Exists(request.Email);
            if (existsDonor) return ResultViewModel<Guid>.Error("Donor with this e-mail already exists");

            var hash = _authService.ComputeHash(request.Password);
            request.Password = hash;
            
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