using MediatR;

namespace ErpSystem.Application.Payments.Queries.GetPaymentById;

public record GetPaymentByIdQuery(Guid PaymentId) : IRequest<PaymentDetailViewModel?>; 