namespace ErpSystem.Frontend.Core.Constants;

public static class PaymentMethodErrorKeys
{
    private static readonly Dictionary<string, string> _translations =
        new()
        {
            ["PaymentMethodNotFound"] = "Начинът на плащане не е намерен",
            ["PaymentMethodRequired"] = "Начинът на плащане е задължителен",
            ["PaymentMethodIsUsedByOrders"] =
                "Не можете да изтриете метод на плащане, който се използва от поръчки",
            ["PaymentMethodIsUsedByPayments"] =
                "Не можете да изтриете метод на плащане, който се използва от плащания",
            ["NameRequired"] = "Името на начина на плащане е задължително",
            ["NameTooLong"] = "Името на начина на плащане не може да бъде по-дълго от 50 символа"
        };

    public static string Translate(string errorKey)
    {
        return _translations.TryGetValue(errorKey, out var translation) ? translation : errorKey;
    }
}
