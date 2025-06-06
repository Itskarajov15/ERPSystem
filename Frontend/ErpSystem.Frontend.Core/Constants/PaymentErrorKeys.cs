namespace ErpSystem.Frontend.Core.Constants;

public static class PaymentErrorKeys
{
    private static readonly Dictionary<string, string> _translations = new()
    {
        ["InvoiceCannotRecordPayment"] = "Не може да се запише плащане за тази фактура. Само издадени фактури без записано плащане могат да приемат плащания",
        ["InvoicePartialPaymentNotAllowed"] = "Частичните плащания не са разрешени. Сумата трябва да бъде равна на общата сума на фактурата",
        ["InvoiceAlreadyFullyPaid"] = "Фактурата вече има записано плащане"
    };

    public static string Translate(string errorKey)
    {
        return _translations.TryGetValue(errorKey, out var translation) 
            ? translation 
            : errorKey;
    }
} 