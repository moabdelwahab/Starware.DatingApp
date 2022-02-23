﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.Core.InfrastructureContracts;
using Starware.DatingApp.Core.PersistenceContracts;
using Starware.DatingApp.Core.ServiceContracts;
using Starware.DatingApp.Persistence;
using Starware.DatingApp.SharedKernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;


        public UserService(IUnitOfWork unitOfWork, 
            IMapper mapper,
            IPhotoService photoService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.photoService = photoService;
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

        public async Task<ApiResponse<PhotoDto>> AddPhoto(IFormFile file,string username)
        {   
            var user = await unitOfWork.UserRepository.GetByUserName(username.ToLower());
            var response = new ApiResponse<PhotoDto>();
            var uploadResponse = await photoService.Upload(file);
            
            var photo = new Photo()
            {
                PublicId= uploadResponse.PublicId,
                Url = uploadResponse.Url.AbsoluteUri,
            };
            
            if (user?.Photos?.Count == 0 )
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);
            await unitOfWork.UserRepository.Update(user);
            response.Data = mapper.Map<PhotoDto>(photo);
            return response;
        }

        public async Task<ApiResponse<bool>> DeletePhoto(string username,string publicId)
        {
            var user = await unitOfWork.UserRepository.GetByUserName(username.ToLower());
            var response = new ApiResponse<bool>();
            var deleteReponse = await photoService.DeletePhoto(publicId);

            var photoToDelete = user.Photos.FirstOrDefault(p => p.PublicId == publicId);
            
            if (photoToDelete != null)
            
            user.Photos.Remove(photoToDelete);
            await unitOfWork.UserRepository.Update(user);
            
            response.Data = true;
            return response;
        }

        public async Task<ApiResponse<bool>> SetMainPhoto(string username, int Id)
        {
            var user = await unitOfWork.UserRepository.GetByUserName(username.ToLower());
            var response = new ApiResponse<bool>();

            if(user != null)
            {
                var userPhoto = user.Photos.FirstOrDefault(x => x.Id == Id);

                if (userPhoto != null)
                {
                    foreach(var photo in user.Photos)
                    {
                        photo.IsMain = false;
                    }
                    userPhoto.IsMain = true;
                }
                var updateResponse = await unitOfWork.UserRepository.Update(user);

                response.Data = updateResponse > 0;
            }
            return response;
        }
    }
}
