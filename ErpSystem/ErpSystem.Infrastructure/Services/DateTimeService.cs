using ErpSystem.Application.Common.Interfaces;

namespace ErpSystem.Infrastructure.Services;

internal class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
