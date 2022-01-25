using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.Domains;

namespace Starware.DatingApp.Persistence
{
    public class DatingAppContext : DbContext
    {
        public DatingAppContext(DbContextOptions options) : base(options)
        {
             
        }
        public DbSet<AppUser> Users { get; set; }
    }
}