using Microsoft.AspNetCore.Identity;

namespace EnergyTracker.Models
{
    public class UserModel : IdentityUser
    {
        public string UserName { get; set; }
    }
}
