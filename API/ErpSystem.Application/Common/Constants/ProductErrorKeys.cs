using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ErpSystem.Application.Common.Constants;

public static class ProductErrorKeys
{
    public const string ProductNotFound = "ProductNotFound";
    public const string ProductRequired = "ProductRequired";
    public const string InsufficientStock = "InsufficientStock";
    public const string ProductStockInsufficient = "ProductStockInsufficient";
    public const string QuantityRequired = "QuantityRequired";
    public const string QuantityInvalid = "QuantityInvalid";
    public const string UnitPriceRequired = "UnitPriceRequired";
    public const string UnitPriceInvalid = "UnitPriceInvalid";
    public const string ProductExistsInOrders = "ProductExistsInOrders";
    public const string ProductExistsInDeliveries = "ProductExistsInDeliveries";
    public const string ProductUsesUnitOfMeasure = "ProductUsesUnitOfMeasure";
}
