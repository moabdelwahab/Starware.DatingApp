using AutoMapper;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.Core.PersistenceContracts;
using Starware.DatingApp.Core.ServiceContracts;
using Starware.DatingApp.Persistence;
using Starware.DatingApp.SharedKernal.Common;
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
        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<int>> AddUser(AppUser user)
        {
            var response = new ApiResponse<int>();

            response.Data = await unitOfWork.UserRepository.Insert(user);

            return response;
        }

        public async Task<ApiResponse<IEnumerable<MemberDto>>> GetAllUser()
        {
            var response = new ApiResponse<IEnumerable<MemberDto>>();

            var users = await unitOfWork.UserRepository.GetUsersWithData();
            
            response.Data = mapper.Map<List<MemberDto>>(users);

            return response;
        }

        public async Task<ApiResponse<AppUser>> GetById(int id)
        {
            var response = new ApiResponse<AppUser>();

            response.Data =  await unitOfWork.UserRepository.GetById(id);
            
            return response;
        }

        public async Task<ApiResponse<MemberDto>> GetMemberByUsername(string username)
        {

            var response = new ApiResponse<MemberDto>();
            var user = await unitOfWork.UserRepository.GetByUserName(username);
            response.Data = mapper.Map<MemberDto>(user);
            return response;

        }
        public async Task<ApiResponse<AppUser>> GetByUsername(string username)
        {
            var reponse = new ApiResponse<AppUser>();
            reponse.Data = await unitOfWork.UserRepository.GetByUserName(username);
            return reponse;
        }

        public async Task<ApiResponse<bool>> UpdateUser(MemberDto userToUpdate)
        {
            var response = new ApiResponse<bool>();

            var dbUser = await unitOfWork.UserRepository.GetByUserName(userToUpdate.UserName);

            var userMapped = mapper.Map<MemberDto,AppUser>(userToUpdate, dbUser);
            
            response.Data  = await unitOfWork.UserRepository.Update(userMapped) > 0 ;

            return response;
        }
    }
}
