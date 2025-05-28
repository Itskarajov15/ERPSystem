using MediatR;

namespace ErpSystem.Application.PaymentMethods.Commands.CreatePaymentMethod;

public record CreatePaymentMethodCommand(string Name) : IRequest<Guid>;
