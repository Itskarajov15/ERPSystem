namespace ErpSystem.Frontend.Core.Constants;

public static class UnitOfMeasureErrorKeys
{
    private static readonly Dictionary<string, string> _translations = new()
    {
        ["UnitOfMeasureNotFound"] = "Мярката не е намерена",
        ["NameRequired"] = "Името е задължително",
        ["NameTooLong"] = "Името не може да бъде по-дълго от 50 символа"
    };

    public static string Translate(string key)
    {
        return _translations.TryGetValue(key, out var translation) ? translation : key;
    }
} 