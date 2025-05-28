using MediatR;

namespace ErpSystem.Application.PaymentMethods.Commands.DeletePaymentMethod;

public record DeletePaymentMethodCommand(Guid Id) : IRequest;
