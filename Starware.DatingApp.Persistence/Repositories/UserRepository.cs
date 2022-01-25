using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.PersistenceContracts;

namespace Starware.DatingApp.Persistence.Repositories
{
    public class UserRepository : Repository<AppUser> , IUserRepository
    {
        public UserRepository(DatingAppContext context) : base(context)
        {
        }
    }
}
