using AutoMapper;
using ErpSystem.Application.Common.Mappings;
using ErpSystem.Domain.Entities.Deliveries;

namespace ErpSystem.Application.Suppliers.DTOs;

public class SupplierDto : IMapFrom<Supplier>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Supplier, SupplierDto>();
    }
}
