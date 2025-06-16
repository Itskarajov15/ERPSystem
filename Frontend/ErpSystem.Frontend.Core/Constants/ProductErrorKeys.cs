namespace ErpSystem.Frontend.Core.Constants;

public static class ProductErrorKeys
{
    private static readonly Dictionary<string, string> _translations =
        new()
        {
            ["ProductNotFound"] = "Продуктът не е намерен",
            ["ProductRequired"] = "Продуктът е задължителен",
            ["InsufficientStock"] = "Недостатъчно количество в склада",
            ["ProductStockInsufficient"] = "Недостатъчно количество от продукта в склада",
            ["QuantityRequired"] = "Количеството е задължително",
            ["QuantityInvalid"] = "Количеството трябва да бъде по-голямо от 0",
            ["UnitPriceRequired"] = "Единичната цена е задължителна",
            ["UnitPriceInvalid"] = "Единичната цена трябва да бъде по-голяма от 0",
            ["ProductExistsInOrders"] =
                "Не можете да изтриете продукт, който се използва в поръчка",
            ["ProductExistsInDeliveries"] =
                "Не можете да изтриете продукт, който се използва в доставка",
        };

    public static string Translate(string errorKey)
    {
        return _translations.TryGetValue(errorKey, out var translation) ? translation : errorKey;
    }
}
