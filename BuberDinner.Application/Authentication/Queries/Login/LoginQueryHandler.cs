using BuberDinner.Application.Common.Authentication;
using BuberDinner.Application.Services.Authentication;
using BurberDinner.Application.Common.Interfaces.Presistence;
using ErrorOr;
using MediatR;
using BuberDinner.Application.Common.Error;
using BurberDiner.Domain.Entities.Entities;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Authentication.Common;

namespace BurberDiner.Application.Authentication.Commands.Register;

public class LoginCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handler    
        (
            LoginQuery query,
            CancellationToken cancellationToken)
    {
        // 1- Validate the user does not exist
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Domain.Common.Errors.Errors.User.DuplicateEmail;
        }
        // 2- Create user (Generate unique ID) & persist to Db 
        if (user.Password != query.Password)
        {
            return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
        }

        // 3- Generate jwt Token
        var token = _tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }

    public Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
