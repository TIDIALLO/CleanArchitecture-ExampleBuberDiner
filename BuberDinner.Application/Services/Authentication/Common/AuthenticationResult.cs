using BurberDiner.Domain.Entities.Entities;
using ErrorOr;
namespace BuberDinner.Application.Services.Authentication;

public record AuthenticationResults(
    User user,
    string Token
);