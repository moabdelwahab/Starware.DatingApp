using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.Domains;

namespace Starware.DatingApp.Persistence.Repositories
{
    internal class UserRepository : Repository<AppUser>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
