using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.Core.Requests;
using Starware.DatingApp.SharedKernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.PersistenceContracts
{
    public interface ILikeRepository
    {
        Task<PagedList<LikeDto>> GetLikes(GetLikesRequest getLikesRequest);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<Like> GetLike(int sourceUserId, int likedUserId);
    }
}