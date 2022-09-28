using BurberDiner.Domain.Entities.Entities;

namespace BuberDinner.Application.Common.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}