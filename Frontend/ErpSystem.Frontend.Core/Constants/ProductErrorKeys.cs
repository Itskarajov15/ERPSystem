using System.Collections.Generic;

namespace ErpSystem.Frontend.Core.Constants;

public static class ProductErrorKeys
{
    private static readonly Dictionary<string, string> _translations = new()
    {
        ["ProductNotFound"] = "Продуктът не е намерен",
        ["NameRequired"] = "Името на продукта е задължително",
        ["NameTooLong"] = "Името на продукта не може да бъде по-дълго от 100 символа",
        ["SkuRequired"] = "SKU е задължителен",
        ["SkuTooLong"] = "SKU не може да бъде по-дълъг от 50 символа",
        ["DescriptionRequired"] = "Описанието е задължително",
        ["DescriptionTooLong"] = "Описанието не може да бъде по-дълго от 500 символа",
        ["UnitPriceGreaterThanZero"] = "Единичната цена трябва да бъде по-голяма от нула",
        ["ReorderLevelGreaterThanOrEqualToZero"] = "Нивото за презареждане трябва да бъде по-голямо или равно на нула",
        ["UnitOfMeasureRequired"] = "Мярката е задължителна",
        ["ProductCreatedSuccessfully"] = "Продуктът е създаден успешно",
        ["ProductUpdatedSuccessfully"] = "Продуктът е обновен успешно",
        ["ProductDeletedSuccessfully"] = "Продуктът е изтрит успешно",
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
        ["ProductUsesUnitOfMeasure"] =
            "Не можете да изтриете мерна единица, която се използва от продукт",
    };

    public static string Translate(string key)
    {
        return _translations.TryGetValue(key, out var translation) ? translation : key;
    }
}
