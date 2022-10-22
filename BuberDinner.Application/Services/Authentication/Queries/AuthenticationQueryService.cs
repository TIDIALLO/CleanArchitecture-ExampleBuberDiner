using BuberDinner.Application.Common.Authentication;
using BuberDinner.Application.Common.Errors;
using BurberDiner.Domain.Common.Errors;
using BurberDiner.Domain.Entities.Entities;
using BurberDinner.Application.Common.Interfaces.Presistence;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationQueryService(
        IJwtTokenGenerator tokenGenerator,
        IUserRepository userRepository
    )
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }

    // public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    // {
    //     // 1- Validate the user does not exist
    //     if (_userRepository.GetUserByEmail(email) is not null)
    //     {
    //         //throw new Exception("User with given email already exist.");
    //         //throw new DuplicateEmailException();
    //         //----------------------------
    //         //gestion des flux avec ErrorOr
    //         return Errors.User.DuplicateEmail;
    //     }
    //     // 2- Create user (Generate unique ID) & persist to Db 
    //     var user = new User
    //     {
    //         FirstName = firstName,
    //         LastName = lastName,
    //         Email = email,
    //         Password = password
    //     };
    //     _userRepository.AddUser(user);

    //     // 3- Generate jwt Token
    //     var token = _tokenGenerator.GenerateToken(user);

    //     return new AuthenticationResult(
    //         user,
    //         token);
    // }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // 1- Validate the user does not exist
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            // throw new Exception("User with given email already exist.");
            return Errors.Authentication.InvalidCredentials;
        }
        // 2- Validate the password is corrrect
        if (user.Password != password)
        {
            // throw new Exception("Password not valid");
            return Errors.Authentication.InvalidCredentials;
        }
        // 3- create token
        var token = _tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}