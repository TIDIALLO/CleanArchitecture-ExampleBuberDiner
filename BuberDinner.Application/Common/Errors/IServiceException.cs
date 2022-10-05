using System.Net;

namespace BurberDiner.Application.Common.Error;

public interface IServiceException{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage {get;}
}