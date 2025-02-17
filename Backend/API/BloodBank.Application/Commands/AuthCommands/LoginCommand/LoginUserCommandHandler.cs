using BloodBank.Application.Models;
using BloodBank.Core.Interfaces;
using BloodBank.Infrastructure.Security;
using MediatR;

namespace BloodBank.Application.Commands.AuthCommands.LoginCommand;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultViewModel<LoginViewModel>>
{
    private readonly IAuthService _authService;
    private readonly IDonorRepository _donorRepository;
    
    public LoginUserCommandHandler(IAuthService authService, IDonorRepository donorRepository)
    {
        _authService = authService;
        _donorRepository = donorRepository;
    }

    public async Task<ResultViewModel<LoginViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _authService.ComputeHash(request.Password);
        
        var user = await _donorRepository.GetDonorByEmailAndPassword(request.Email, passwordHash);
    }
}