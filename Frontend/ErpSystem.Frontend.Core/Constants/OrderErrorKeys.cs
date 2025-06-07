namespace ErpSystem.Frontend.Core.Constants;

public static class OrderErrorKeys
{
    private static readonly Dictionary<string, string> _translations =
        new()
        {
            ["OrderNotFound"] = "Поръчката не е намерена",
            ["OrderCannotBeCompleted"] =
                "Поръчката не може да бъде завършена. Само чакащи поръчки могат да бъдат завършени",
            ["OrderCannotBeCanceled"] =
                "Поръчката не може да бъде отменена. Само чакащи поръчки могат да бъдат отменени",
            ["OrderCannotBeDeleted"] =
                "Поръчката не може да бъде изтрита. Само чакащи поръчки могат да бъдат изтрити",
            ["OrderItemNotFound"] = "Артикулът от поръчката не е намерен",
            ["OrderItemsRequired"] = "Поне един артикул е задължителен",
        };

    public static string Translate(string errorKey)
    {
        return _translations.TryGetValue(errorKey, out var translation) ? translation : errorKey;
    }
}
