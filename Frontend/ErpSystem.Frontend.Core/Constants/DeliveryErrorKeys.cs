namespace ErpSystem.Frontend.Core.Constants;

public static class DeliveryErrorKeys
{
    private static readonly Dictionary<string, string> _translations = new()
    {
        ["DeliveryNotFound"] = "Доставката не е намерена",
        ["DeliveryCannotBeStarted"] = "Доставката не може да бъде започната. Само регистрирани доставки могат да бъдат започнати",
        ["DeliveryCannotBeCompleted"] = "Доставката не може да бъде завършена. Само доставки в обработка могат да бъдат завършени",
        ["DeliveryCannotBeDeleted"] = "Доставката не може да бъде изтрита. Само регистрирани доставки могат да бъдат изтрити",
        ["DeliveryItemNotFound"] = "Артикулът от доставката не е намерен",
        ["DeliveryItemsRequired"] = "Поне един артикул е задължителен",
        ["DeliveryDateRequired"] = "Датата на доставка е задължителна"
    };

    public static string Translate(string errorKey)
    {
        return _translations.TryGetValue(errorKey, out var translation) 
            ? translation 
            : errorKey;
    }
} 