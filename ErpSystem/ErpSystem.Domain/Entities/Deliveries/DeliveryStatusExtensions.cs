namespace ErpSystem.Domain.Entities.Deliveries;

public static class DeliveryStatusExtensions
{
    public static string ToDisplayName(this DeliveryStatus status)
    {
        return status switch
        {
            DeliveryStatus.Registered => "Регистрирана",
            DeliveryStatus.InProgress => "В обработка",
            DeliveryStatus.Completed => "Завършена",
            _ => status.ToString(),
        };
    }
}
