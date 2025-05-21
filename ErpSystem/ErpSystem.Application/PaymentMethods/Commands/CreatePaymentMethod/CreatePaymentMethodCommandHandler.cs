using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.PaymentMethods.Commands.CreatePaymentMethod;

internal class CreatePaymentMethodCommandHandler : IRequestHandler<CreatePaymentMethodCommand, Guid>
{
    private readonly IRepository _repository;

    public CreatePaymentMethodCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreatePaymentMethodCommand request,
        CancellationToken cancellationToken
    )
    {
        var paymentMethod = new PaymentMethod { Name = request.Name };

        await _repository.AddAsync(paymentMethod);
        await _repository.SaveChangesAsync();

        return paymentMethod.Id;
    }
}
