namespace ErpSystem.Application.Common.Exceptions;

public class InsufficientStockException : ArgumentException
{
    public InsufficientStockException(string message)
        : base(message) { }

    public InsufficientStockException(string message, Exception innerException)
        : base(message, innerException) { }
}
