using ErpSystem.Application.PaymentMethods.DTOs;
using MediatR;

namespace ErpSystem.Application.PaymentMethods.Queries.GetPaymentMethodById;

public record GetPaymentMethodByIdQuery(Guid Id) : IRequest<PaymentMethodDto>;
