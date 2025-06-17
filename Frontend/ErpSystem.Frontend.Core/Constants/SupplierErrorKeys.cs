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
            ["NameRequired"] = "Името е задължително",
            ["NameTooLong"] = "Името не може да бъде по-дълго от 100 символа",
            ["AddressRequired"] = "Адресът е задължителен",
            ["AddressTooLong"] = "Адресът не може да бъде по-дълъг от 200 символа",
            ["PhoneRequired"] = "Телефонният номер е задължителен",
            ["PhoneTooLong"] = "Телефонният номер не може да бъде по-дълъг от 15 символа",
            ["EmailRequired"] = "Имейлът е задължителен",
            ["EmailInvalid"] = "Невалиден имейл формат",
            ["ContactPersonRequired"] = "Лицето за контакт е задължително",
            ["ContactPersonTooLong"] = "Името на лицето за контакт не може да бъде по-дълго от 100 символа"
        };

    public static string Translate(string errorKey)
    {
        return _translations.TryGetValue(errorKey, out var translation) ? translation : errorKey;
    }
}
