using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.Core.PersistenceContracts;
using Starware.DatingApp.Core.Requests;
using Starware.DatingApp.SharedKernal.Common;

namespace Starware.DatingApp.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatingAppContext context;
        private readonly IMapper mapper;

        public UserRepository(DatingAppContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> Delete(AppUser entity)
        {
            context.Users.Remove(entity);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<AppUser> GetById(int id)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Id== id);
        }

        public async Task<AppUser> GetByUserName(string userName)
        {
            return await context.Users.Include(u => u.Photos).Include(u => u.UserLikes).Where(x => x.UserName.ToLower() == userName.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<PagedList<MemberDto>> GetUsersWithData(GetAllUsersRequest getAllUsersRequest)
        {
            var query = context.Users.Include(x => x.Photos).AsQueryable();

            if (!string.IsNullOrEmpty(getAllUsersRequest.Gender))
            {
                query = query.Where(x => x.Gender.ToLower() == getAllUsersRequest.Gender.ToLower());
            }

            if (!string.IsNullOrEmpty(getAllUsersRequest.Name))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(getAllUsersRequest.Name.ToLower()));
            }

            if (getAllUsersRequest.FromAge > 0 && getAllUsersRequest.ToAge > 0)
            {
                var minAge = DateTime.Today.AddYears(-getAllUsersRequest.FromAge.Value);
                var maxAgeDate = DateTime.Today.AddYears(-getAllUsersRequest.ToAge.Value - 1);
                query = query.Where(x => x.BirthDate.Date <= minAge && x.BirthDate.Date >= maxAgeDate);
            }

            query = getAllUsersRequest.OrderBy switch
            {
                "lastActive" => query.OrderByDescending(x => x.LastActive),
                _ => query.OrderByDescending(x => x.CreationDate)
            };

            var items = await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(mapper.ConfigurationProvider), getAllUsersRequest.PageNumber, getAllUsersRequest.PageSize);

            return items;
        }

        public async Task<int> Insert(AppUser entity)
        {
            var result =  context.Users.Add(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<DateTime> LogUserActivity(string userName)
        {
            var user = await context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            user.LastActive = DateTime.Now;
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user.LastActive;
        }

        public async Task<int> Update(AppUser entity)
        {
            context.Users.Update(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }
    }
}
