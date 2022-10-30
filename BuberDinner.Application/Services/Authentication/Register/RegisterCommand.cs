
namespace BuberDinner.Application.Services.Authentication.Register;

public class RegisterCommand
{
    private string firstName;
    private string lastName;
    private string email;
    private string password;

    public RegisterCommand(string firstName, string lastName, string email, string password)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;
    }
}