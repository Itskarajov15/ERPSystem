using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.PaymentMethods.Commands.DeletePaymentMethod;

internal class DeletePaymentMethodCommandHandler : IRequestHandler<DeletePaymentMethodCommand>
{
    private readonly IRepository _repository;

    public DeletePaymentMethodCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        DeletePaymentMethodCommand request,
        CancellationToken cancellationToken
    )
    {
        await _repository.SoftDeleteById<PaymentMethod>(request.Id);
        await _repository.SaveChangesAsync();
    }
}
