namespace ErpSystem.Frontend.Core.Constants;

public static class PaymentMethodErrorKeys
{
    private static readonly Dictionary<string, string> _translations = new()
    {
        ["PaymentMethodNotFound"] = "Начинът на плащане не е намерен",
        ["PaymentMethodRequired"] = "Начинът на плащане е задължителен"
    };

    public static string Translate(string errorKey)
    {
        return _translations.TryGetValue(errorKey, out var translation) 
            ? translation 
            : errorKey;
    }
} 