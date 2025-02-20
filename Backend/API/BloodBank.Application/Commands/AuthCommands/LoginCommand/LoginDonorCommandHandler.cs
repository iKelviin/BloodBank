using BloodBank.Application.Models;
using BloodBank.Core.Interfaces;
using BloodBank.Infrastructure.Security;
using MediatR;

namespace BloodBank.Application.Commands.AuthCommands.LoginCommand;

public class LoginDonorCommandHandler : IRequestHandler<LoginDonorCommand, ResultViewModel<LoginViewModel>>
{
    private readonly IAuthService _authService;
    private readonly IDonorRepository _donorRepository;
    
    public LoginDonorCommandHandler(IAuthService authService, IDonorRepository donorRepository)
    {
        _authService = authService;
        _donorRepository = donorRepository;
    }

    public async Task<ResultViewModel<LoginViewModel>> Handle(LoginDonorCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _authService.ComputeHash(request.Password);
        
        var user = await _donorRepository.GetDonorByEmailAndPassword(request.Email, passwordHash);
        
        if(user is null) return ResultViewModel<LoginViewModel>.Error("User not found");
        
        var token = _authService.GenerateToken(user);
        var viewModel = new LoginViewModel(token);
        return ResultViewModel<LoginViewModel>.Success(viewModel);
    }
}