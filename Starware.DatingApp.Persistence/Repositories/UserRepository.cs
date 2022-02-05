using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.PersistenceContracts;

namespace Starware.DatingApp.Persistence.Repositories
{
    public class UserRepository : Repository<AppUser> , IUserRepository
    {
        private readonly DatingAppContext context;

        public UserRepository(DatingAppContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<AppUser> GetByUserName(string userName)
        {
            return await context.Users.Where(x => x.UserName.ToLower() == userName.ToLower()).FirstOrDefaultAsync();
        }
    }
}
