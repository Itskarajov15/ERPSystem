using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.UnitsOfMeasure;

public class UnitOfMeasureEditModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Името е задължително")]
    [MaxLength(50, ErrorMessage = "Името не трябва да надвишава 50 символа")]
    public string Name { get; set; } = string.Empty;
}
