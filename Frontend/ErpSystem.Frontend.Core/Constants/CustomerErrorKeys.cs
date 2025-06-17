namespace ErpSystem.Frontend.Core.Constants;

public static class CustomerErrorKeys
{
    private static readonly Dictionary<string, string> _translations =
        new()
        {
            ["CustomerNotFound"] = "Клиентът не е намерен",
            ["CustomerRequired"] = "Клиентът е задължителен",
            ["CustomerExistsInOrder"] = "Не можете да изтриете клиент, който участва в поръчка",
            ["NameRequired"] = "Името е задължително",
            ["NameTooLong"] = "Името не може да бъде по-дълго от 100 символа",
            ["AddressRequired"] = "Адресът е задължителен",
            ["AddressTooLong"] = "Адресът не може да бъде по-дълъг от 200 символа",
            ["PhoneRequired"] = "Телефонният номер е задължителен",
            ["PhoneTooLong"] = "Телефонният номер не може да бъде по-дълъг от 15 символа",
            ["EmailRequired"] = "Имейлът е задължителен",
            ["EmailInvalid"] = "Невалиден имейл формат",
            ["ContactNameRequired"] = "Името на лицето за контакт е задължително",
            ["ContactNameTooLong"] = "Името на лицето за контакт не може да бъде по-дълго от 100 символа"
        };

    public static string Translate(string errorKey)
    {
        return _translations.TryGetValue(errorKey, out var translation) ? translation : errorKey;
    }
}
