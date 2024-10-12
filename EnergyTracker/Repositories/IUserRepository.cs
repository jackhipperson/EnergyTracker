using Microsoft.AspNetCore.Identity;

namespace EnergyTracker.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> AddUser(IdentityUser user, string password);
    }
}
