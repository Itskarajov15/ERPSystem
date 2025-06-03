using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Deliveries;

public class DeliveryEditModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Доставчикът е задължителен")]
    [Display(Name = "Доставчик")]
    public Guid SupplierId { get; set; }

    [Required(ErrorMessage = "Датата на доставка е задължителна")]
    [Display(Name = "Дата на доставка")]
    public DateTime DeliveryDate { get; set; } = DateTime.Today;

    [Display(Name = "Коментар")]
    [StringLength(500, ErrorMessage = "Коментарът не може да бъде по-дълъг от 500 символа")]
    public string? Comment { get; set; }

    [Display(Name = "Артикули")]
    [Required(ErrorMessage = "Поне един артикул е задължителен")]
    [MinLength(1, ErrorMessage = "Поне един артикул е задължителен")]
    public List<DeliveryItemEditModel> Items { get; set; } = new();
}
