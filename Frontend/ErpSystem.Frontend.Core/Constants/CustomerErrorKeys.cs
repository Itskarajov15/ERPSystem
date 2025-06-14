namespace ErpSystem.Frontend.Core.Constants;

public static class CustomerErrorKeys
{
    private static readonly Dictionary<string, string> _translations =
        new()
        {
            ["CustomerNotFound"] = "Клиентът не е намерен",
            ["CustomerRequired"] = "Клиентът е задължителен",
        };

    public static string Translate(string errorKey)
    {
        return _translations.TryGetValue(errorKey, out var translation) ? translation : errorKey;
    }
}
