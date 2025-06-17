namespace ErpSystem.Application.Common.Constants;

public static class PaymentMethodErrorKeys
{
    public const string PaymentMethodNotFound = "PaymentMethodNotFound";
    public const string PaymentMethodRequired = "PaymentMethodRequired";
    public const string PaymentMethodIsUsedByOrders = "PaymentMethodIsUsedByOrders";
    public const string PaymentMethodIsUsedByPayments = "PaymentMethodIsUsedByPayments";
    public const string NameRequired = "NameRequired";
    public const string NameTooLong = "NameTooLong";
}
