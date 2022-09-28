using BuberDinner.Application.Common.Interfaces.Services;

namespace BuberDinner.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    // DateTime IDateTimeProvider.UtcNow => throw new NotImplementedException();
    public DateTime UtcNow => DateTime.UtcNow;
}