using Microsoft.AspNetCore.Mvc;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Application.Services.Authentication;
using ErrorOr;
 
using BurberDiner.Domain.Common.Errors;
using static BurberDiner.Domain.Common.Errors.Errors;

//using BuberDinner.Api.Controllers;

namespace BuberDinner.Api.Controllers;

[Route("auth")]
// [ErrorHandlingFilter]
public class AuthenticatorController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticatorController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
        // Utilisation avec Match
        // return authResult.Match(
        //     authResult => Ok(NewMethod(authResult)),
        //     _ => Problem(
        //         statusCode: StatusCodes.Status409Conflict,
        //         title: "User already exist")
        // );
        //****************************************************************************

        // Utilisation MatchFirst
        // return authResult.MatchFirst(
        //     authResult => Ok(MapAuthResult(authResult)),
        //     firtError => Problem(
        //                     statusCode: StatusCodes.Status409Conflict,
        //                     title: firtError.Description)
        // );
        //=================       Using ApiController class   ===========================================
        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
        );
        // var response = new AuthenticationResponse(
        //     authResult.user.Id,
        //     authResult.user.FirstName,
        //     authResult.user.LastName,
        //     authResult.user.Email,
        //     authResult.Token
        // );

        // return Ok(response);
    }



    // private static AuthenticationResponse NewMethod(AuthenticationResult authResult)
    // {
    //     return  new AuthenticationResponse(
    //         authResult.user.Id,
    //         authResult.user.FirstName,
    //         authResult.user.LastName,
    //         authResult.user.Email,
    //         authResult.Token);
    // }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationService.Login(
           request.Email,
           request.Password);

        var response = _authenticationService.Login(
            request.Email,
            request.Password
        );
        //     authResult.user.Id,
        //     authResult.user.FirstName,
        //     authResult.user.LastName,
        //     authResult.user.Email,
        //     authResult.Token
        // ); Authentication.InvalidCredentials
        //return Ok(response);
        if (authResult.IsError
            && authResult.FirstError == Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
        );


    }
    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.user.Id,
            authResult.user.FirstName,
            authResult.user.LastName,
            authResult.user.Email,
            authResult.Token);
    }
}