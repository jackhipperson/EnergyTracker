using EnergyTracker.Data;
using Microsoft.AspNetCore.Identity;

namespace EnergyTracker.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public UserRepository(AppDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<bool> AddUser(IdentityUser user, string password)
        {
            var identityUser = await userManager.CreateAsync(user, password);
            if (identityUser.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
