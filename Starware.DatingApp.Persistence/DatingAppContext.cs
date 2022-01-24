using Microsoft.EntityFrameworkCore;

namespace Starware.DatingApp.Persistence
{
    public class DatingAppContext : DbContext
    {
        public DatingAppContext(DbContextOptions options) : base(options)
        {

        }
    }
}