using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EnergyTracker.Models.ViewModels
{
    public class UserViewModel : IdentityUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
