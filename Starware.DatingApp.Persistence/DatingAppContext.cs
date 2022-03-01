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
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Like>().HasKey(k => new { k.SourceUserId , k.LikedUserId});
            modelBuilder.Entity<Like>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.UserLikes)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Like>()
                .HasOne(s => s.LikedUser)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}