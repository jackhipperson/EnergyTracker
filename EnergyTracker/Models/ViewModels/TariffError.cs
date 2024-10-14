using System.ComponentModel.DataAnnotations;

namespace EnergyTracker.Models.ViewModels
{
    public class TariffError
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Tariffs Description")]
        public string Description { get; set; }
        [Display(Name = "Tariffs Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Tariffs End Date")]
        public DateTime? EndDate { get; set; }
        public Guid UserId { get; set; }
    }
}
