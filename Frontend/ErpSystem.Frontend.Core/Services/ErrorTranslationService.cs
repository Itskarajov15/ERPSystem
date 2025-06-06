using System.Text.Json;
using ErpSystem.Frontend.Core.Constants;

namespace ErpSystem.Frontend.Core.Services;

public class ErrorTranslationService
{
    public string Translate(string errorMessage)
    {
        var errorKey = ExtractErrorKey(errorMessage);

        var translation = OrderErrorKeys.Translate(errorKey);
        if (translation != errorKey)
            return translation;

        translation = DeliveryErrorKeys.Translate(errorKey);
        if (translation != errorKey)
            return translation;

        translation = ProductErrorKeys.Translate(errorKey);
        if (translation != errorKey)
            return translation;

        translation = SupplierErrorKeys.Translate(errorKey);
        if (translation != errorKey)
            return translation;

        translation = CustomerErrorKeys.Translate(errorKey);
        if (translation != errorKey)
            return translation;

        translation = PaymentMethodErrorKeys.Translate(errorKey);
        if (translation != errorKey)
            return translation;

        translation = PaymentErrorKeys.Translate(errorKey);
        if (translation != errorKey)
            return translation;

        translation = InvoiceErrorKeys.Translate(errorKey);
        if (translation != errorKey)
            return translation;

        return errorKey;
    }

    private string ExtractErrorKey(string errorMessage)
    {
        try
        {
            var jsonStart = errorMessage.IndexOf('{');
            if (jsonStart >= 0)
            {
                var jsonPart = errorMessage.Substring(jsonStart);
                var errorResponse = JsonSerializer.Deserialize<JsonElement>(jsonPart);

                if (errorResponse.TryGetProperty("message", out var messageProperty))
                {
                    return messageProperty.GetString() ?? errorMessage;
                }
            }

            return errorMessage;
        }
        catch
        {
            return errorMessage;
        }
    }
}
