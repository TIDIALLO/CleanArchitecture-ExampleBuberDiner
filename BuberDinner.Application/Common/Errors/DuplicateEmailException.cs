using System.Net;
using BurberDiner.Application.Common.Error;

namespace BuberDinner.Application.Common.Errors;

public class DuplicateEmailException : Exception, IServiceException
{
    public DuplicateEmailException()
    {
    }

    public DuplicateEmailException(string message) : base(message)
    {

    }

    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => "Email Already existe";
}