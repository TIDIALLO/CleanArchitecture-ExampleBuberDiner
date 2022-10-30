using BuberDinner.Application.Common.Authentication;
using BuberDinner.Application.Services.Authentication;
using BurberDinner.Application.Common.Interfaces.Presistence;
using ErrorOr;
using MediatR;
using BuberDinner.Application.Authentication.Common;
using BurberDiner.Domain.Entities.Entities;

namespace BurberDiner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(
        IJwtTokenGenerator tokenGenerator, 
        IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle
    (
        RegisterCommand command, 
        CancellationToken cancellationToken
    )
    {
        // 1- Validate the user does not exist
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Domain.Common.Errors.Errors.User.DuplicateEmail;
        }
        // 2- Create user (Generate unique ID) & persist to Db 
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };
        _userRepository.AddUser(user);

        // 3- Generate jwt Token
        var token = _tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}
