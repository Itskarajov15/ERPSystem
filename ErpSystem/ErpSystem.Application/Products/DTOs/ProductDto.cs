using AutoMapper;
using ErpSystem.Application.Common.Mappings;
using ErpSystem.Domain.Entities.Inventory;

namespace ErpSystem.Application.Products.DTOs;

public class ProductDto : IMapFrom<Product>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string SKU { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string UnitOfMeasure { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public decimal ReorderLevel { get; set; }

    public bool IsLowStock => Quantity < ReorderLevel;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDto>();
    }
}
