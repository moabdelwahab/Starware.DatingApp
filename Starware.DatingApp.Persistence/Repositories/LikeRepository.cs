using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.Core.PersistenceContracts;
using Starware.DatingApp.Core.Requests;
using Starware.DatingApp.SharedKernal.Common;
using Starware.DatingApp.SharedKernal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Persistence.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DatingAppContext context;

        public LikeRepository(DatingAppContext Context)
        {
            this.context = Context;
        }

        public async Task<Like> GetLike(int sourceUserId, int likedUserId)
        {
            return await this.context.Likes.FindAsync(sourceUserId, likedUserId);
        }

        public async Task<PagedList<LikeDto>> GetLikes(GetLikesRequest getLikesRequest)
        {
            var users = this.context.Users.OrderBy(x => x.UserName).AsQueryable();
            var likes = this.context.Likes.AsQueryable();

            if (getLikesRequest.Predicate == "Likes")
            {
                likes = likes.Where(like => like.SourceUserId == getLikesRequest.UserId);
                users = likes.Select(like => like.LikedUser);
            }
            if (getLikesRequest.Predicate == "LikedBy")
            {
                likes = likes.Where(like => like.LikedUserId == getLikesRequest.UserId);
                users = likes.Select(like => like.SourceUser);
            }

            var likedUsers = users.Select(user => new LikeDto
            {

                Id = user.Id,
                Age = user.BirthDate.GetAgeFromDate(),
                City = user.City,
                Country = user.Country,
                Name = $"{user.FirstName} {user.MiddleName} {user.LastName}",
                KnowAs = user.KnownAs,
                PhotoUrl = user.Photos.Where(photo => photo.IsMain).FirstOrDefault().Url,
                LastActive = user.LastActive,
                Username = user.UserName

            });

            return await PagedList<LikeDto>.CreateAsync(likedUsers, getLikesRequest.PageNumber, getLikesRequest.PageSize);
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await context.Users
                .Include(x => x.UserLikes)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
