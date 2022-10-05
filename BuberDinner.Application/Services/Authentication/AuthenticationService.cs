using BuberDinner.Application.Common.Authentication;
using BuberDinner.Application.Common.Errors;
using BurberDiner.Domain.Entities.Entities;
using BurberDinner.Application.Common.Interfaces.Presistence;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationService(
        IJwtTokenGenerator tokenGenerator,
        IUserRepository userRepository
    )
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // 1- Validate the user does not exist
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            //throw new Exception("User with given email already exist.");
            throw new DuplicateEmailException();
        }
        // 2- Create user (Generate unique ID) & persist to Db 
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRepository.AddUser(user);

        // 3- Generate jwt Token
        var token = _tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        // 1- Validate the user does not exist
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User with given email already exist.");
        }
        // 2- Validate the password is corrrect
        if (user.Password != password)
        {
            throw new Exception("Password not valid");
        }
        // 3- create token
        var token = _tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}