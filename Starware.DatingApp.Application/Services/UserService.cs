using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.PersistenceContracts;
using Starware.DatingApp.Core.ServiceContracts;
using Starware.DatingApp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> AddUser(AppUser user)
        {
            return await unitOfWork.UserRepository.Insert(user);
        }

        public async Task<IEnumerable<AppUser>> GetAllUser()
        {
            return await unitOfWork.UserRepository.GetAll();
        }

        public async Task<AppUser> GetById(int id)
        {
            return await unitOfWork.UserRepository.GetById(id);
        }

        public async Task<AppUser> GetByUsername(string username)
        {
            return await unitOfWork.UserRepository.GetByUserName(username);
        }
    }
}
