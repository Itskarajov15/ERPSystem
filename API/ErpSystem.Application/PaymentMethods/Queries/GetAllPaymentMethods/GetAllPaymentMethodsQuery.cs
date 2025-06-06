using ErpSystem.Application.PaymentMethods.DTOs;
using MediatR;

namespace ErpSystem.Application.PaymentMethods.Queries.GetAllPaymentMethods;

public record GetAllPaymentMethodsQuery : IRequest<List<PaymentMethodDto>>; 