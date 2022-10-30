
using BurberDiner.Domain.Entities.Entities;

namespace BuberDinner.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);