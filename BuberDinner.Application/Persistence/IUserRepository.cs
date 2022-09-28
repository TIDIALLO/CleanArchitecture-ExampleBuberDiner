using BurberDiner.Domain.Entities.Entities;

namespace BurberDinner.Application.Common.Interfaces.Presistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void AddUser(User user);

}