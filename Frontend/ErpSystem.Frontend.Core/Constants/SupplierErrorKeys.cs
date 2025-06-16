namespace ErpSystem.Frontend.Core.Constants;

public static class SupplierErrorKeys
{
    private static readonly Dictionary<string, string> _translations =
        new()
        {
            ["SupplierNotFound"] = "Доставчикът не е намерен",
            ["SupplierRequired"] = "Доставчикът е задължителен",
            ["SupplierExistsInDelivery"] =
                "Не можете да изтриете доставчик, който участва в доставка",
        };

    public static string Translate(string errorKey)
    {
        return _translations.TryGetValue(errorKey, out var translation) ? translation : errorKey;
    }
}
