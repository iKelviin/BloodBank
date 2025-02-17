using BloodBank.Application.Models;
using MediatR;

namespace BloodBank.Application.Commands.AuthCommands.LoginCommand;

public class LoginUserCommand : IRequest<ResultViewModel<LoginViewModel>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}