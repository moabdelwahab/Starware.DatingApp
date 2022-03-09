using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.Domains;

namespace Starware.DatingApp.Persistence
{
    public class DatingAppContext : IdentityDbContext<AppUser,AppRole,int
        ,IdentityUserClaim<int>,AppUserRole,IdentityUserLogin<int>
        ,IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public DatingAppContext(DbContextOptions options) : base(options)
        {
             
        }

        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Message { get; set; }


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


            modelBuilder.Entity<Message>()
            .HasOne(s => s.Sender)
            .WithMany(l => l.MessagesSent)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Message>()
            .HasOne(s => s.Recipient)
            .WithMany(l => l.MessagesRecived)
            .HasForeignKey(x => x.RecipientId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUserRole>()
                .HasOne(r => r.AppUser)
                .WithMany(user => user.UserRoles)
                .HasForeignKey(x => x.UserId);



            modelBuilder.Entity<AppUserRole>()
                .HasOne(r => r.AppRole)
                .WithMany(role => role.RoleUsers)
                .HasForeignKey(x => x.RoleId);
        }

    }
}