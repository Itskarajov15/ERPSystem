namespace ErpSystem.Frontend.Core.Constants;

public static class InvoiceErrorKeys
{
    private static readonly Dictionary<string, string> _translations = new()
    {
        ["InvoiceNotFound"] = "Фактурата не е намерена",
        ["InvoiceCannotBeCancelled"] = "Фактурата не може да бъде отменена",
        ["InvoiceCannotBeIssued"] = "Фактурата не може да бъде издадена",
        ["InvoiceCannotBeMarkedAsPaid"] = "Фактурата не може да бъде маркирана като платена",
        ["OrderCannotGenerateInvoice"] = "Не може да се генерира фактура за тази поръчка",
        ["InvoiceAlreadyExists"] = "Фактурата вече съществува",

    };

    public static string Translate(string errorKey)
    {
        return _translations.TryGetValue(errorKey, out var translation) 
            ? translation 
            : errorKey;
    }
} 