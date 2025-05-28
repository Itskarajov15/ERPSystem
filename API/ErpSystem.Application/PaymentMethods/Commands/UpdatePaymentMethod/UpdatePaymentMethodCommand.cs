using MediatR;

namespace ErpSystem.Application.PaymentMethods.Commands.UpdatePaymentMethod;

public record UpdatePaymentMethodCommand(Guid Id, string Name) : IRequest;
