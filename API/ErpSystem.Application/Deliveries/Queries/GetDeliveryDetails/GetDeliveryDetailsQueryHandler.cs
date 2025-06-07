using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Application.Deliveries.DTOs;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Deliveries.Queries.GetDeliveryDetails;

internal class GetDeliveryDetailsQueryHandler
    : IRequestHandler<GetDeliveryDetailsQuery, DeliveryDetailDto>
{
    private IRepository _repository;

    public GetDeliveryDetailsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeliveryDetailDto> Handle(
        GetDeliveryDetailsQuery request,
        CancellationToken cancellationToken
    )
    {
        var delivery = await _repository
            .AllReadOnly<Delivery>()
            .Include(d => d.Supplier)
            .Include(d => d.DeliveryItems)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(d => d.Id == request.Id);

        if (delivery == null)
        {
            throw new NotFoundException(DeliveryErrorKeys.DeliveryNotFound, request.Id);
        }

        return new DeliveryDetailDto()
        {
            Id = delivery.Id,
            DeliveryDate = delivery.DeliveryDate.ToString("dd/MM/yyyy"),
            StatusName = delivery.DeliveryStatus.ToDisplayName(),
            SupplierId = delivery.SupplierId,
            SupplierName = delivery.Supplier.Name,
            DeliveryNumber = delivery.DeliveryNumber,
            Comment = delivery.Comment,
            Status = delivery.DeliveryStatus,
            Items = delivery
                .DeliveryItems.Select(i => new DeliveryItemDetailDto()
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Sku = i.Product.Sku,
                })
                .ToList(),
        };
    }
}
