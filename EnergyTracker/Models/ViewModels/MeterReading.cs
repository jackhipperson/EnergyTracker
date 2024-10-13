using System.ComponentModel.DataAnnotations;

namespace EnergyTracker.Models.ViewModels
{
    public class MeterReading
    {
        [Required]
        [Display(Name = "Reading Date")]
        public DateTime ReadingDate { get; set; }
        [Display(Name = "Electic Reading")]
        public decimal? ElectricReading { get; set; }
        [Display(Name = "Gas Reading")]
        public decimal? GasReading { get; set; }
    }
}
