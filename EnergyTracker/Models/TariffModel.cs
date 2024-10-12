using System.ComponentModel.DataAnnotations;

namespace EnergyTracker.Models;

public class TariffModel
{
    public Guid Id { get; set; }
    [Required]
    [Display(Name="Tariffs Description")]
    public string Description { get; set; }
    [Display(Name = "Gas Unit Rate")]
    public decimal? GasUnitRate { get; set; }
    [Display(Name = "Gas Standing Rate")]
    public decimal? GasStandingRate { get; set; }
    [Display(Name = "Electricity Unit Rate")]
    public decimal? ElectricUnitRate { get; set; }
    [Display(Name = "Electricity Standing Rate")]
    public decimal? ElectricStandingRate { get; set; }
    [Display(Name = "Tariffs Start Date")]
    public DateTime StartDate { get; set; }
    [Display(Name = "Tariffs End Date")]
    public DateTime? EndDate { get; set; }
    public Guid UserId { get; set; }

}
