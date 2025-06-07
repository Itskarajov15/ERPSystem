using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Deliveries;

public class DeliveryCreateModel
{
    [Required(ErrorMessage = "Доставчикът е задължителен")]
    [Display(Name = "Доставчик")]
    public Guid SupplierId { get; set; }

    [Required(ErrorMessage = "Номерът на доставката е задължителен")]
    [Display(Name = "Номер на доставка")]
    [StringLength(
        50,
        ErrorMessage = "Номерът на доставката не може да бъде по-дълъг от 50 символа"
    )]
    public string DeliveryNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Датата на доставка е задължителна")]
    [Display(Name = "Дата на доставка")]
    public DateTime DeliveryDate { get; set; } = DateTime.Today;

    [Display(Name = "Коментар")]
    [StringLength(500, ErrorMessage = "Коментарът не може да бъде по-дълъг от 500 символа")]
    public string? Comment { get; set; }

    [Display(Name = "Артикули")]
    [Required(ErrorMessage = "Поне един артикул е задължителен")]
    [MinLength(1, ErrorMessage = "Поне един артикул е задължителен")]
    public List<DeliveryItemCreateModel> Items { get; set; } = new();
}
