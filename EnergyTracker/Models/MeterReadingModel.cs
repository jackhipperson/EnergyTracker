using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EnergyTracker.Models;

public class MeterReadingModel
{
    public Guid Id { get; set; }
    [Required]
    [Display(Name="Reading Date")]
    public DateTime ReadingDate { get; set; }
    [Display(Name="Electic Reading")]
    public decimal? ElectricReading { get; set; }
    [Display(Name="Gas Reading")]
    public decimal? GasReading { get; set; }
    public Guid UserId { get; set; }
}
