using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Products;

public class ProductEditModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Името е задължително")]
    [Display(Name = "Име")]
    [StringLength(100, ErrorMessage = "Името не може да бъде по-дълго от 100 символа")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "СКУ е задължителен")]
    [Display(Name = "СКУ")]
    [StringLength(50, ErrorMessage = "СКУ не може да бъде по-дълъг от 50 символа")]
    public string Sku { get; set; } = string.Empty;

    [Display(Name = "Описание")]
    [StringLength(500, ErrorMessage = "Описанието не може да бъде по-дълго от 500 символа")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Цената е задължителна")]
    [Display(Name = "Цена")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Цената трябва да бъде по-голяма от 0")]
    public decimal UnitPrice { get; set; }

    [Required(ErrorMessage = "Минималното количество е задължително")]
    [Display(Name = "Минимално количество")]
    [Range(0, double.MaxValue, ErrorMessage = "Минималното количество не може да бъде отрицателно")]
    public decimal ReorderLevel { get; set; }

    [Required(ErrorMessage = "Мерната единица е задължителна")]
    [Display(Name = "Мерна единица")]
    public Guid UnitOfMeasureId { get; set; }
}
