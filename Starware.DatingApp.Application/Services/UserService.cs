using AutoMapper;
using Microsoft.AspNetCore.Http;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs;
using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.Core.InfrastructureContracts;
using Starware.DatingApp.Core.PersistenceContracts;
using Starware.DatingApp.Core.Requests;
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
            IPhotoService photoService
            )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.photoService = photoService;
        }

        /// <summary>
        /// Members
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ApiResponse<int>> AddUser(AppUser user)
        {
            var response = new ApiResponse<int>();

            response.Data = await unitOfWork.UserRepository.Insert(user);

            return response;
        }

        public async Task<ApiResponse<PagedList<MemberDto>>> GetAllUser(GetAllUsersRequest getAllUsersRequest)
        {
            var response = new ApiResponse<PagedList<MemberDto>>();

            response.Data = await unitOfWork.UserRepository.GetUsersWithData(getAllUsersRequest);

            return response;
        }

        public async Task<ApiResponse<AppUser>> GetById(int id)
        {
            var response = new ApiResponse<AppUser>();

            response.Data = await unitOfWork.UserRepository.GetById(id);

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

            var userMapped = mapper.Map<MemberDto, AppUser>(userToUpdate, dbUser);

            response.Data = await unitOfWork.UserRepository.Update(userMapped) > 0;

            return response;
        }
        /// <summary>
        /// Photos
        /// </summary>
        /// <param name="file"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<ApiResponse<PhotoDto>> AddPhoto(IFormFile file, string username)
        {
            var user = await unitOfWork.UserRepository.GetByUserName(username.ToLower());
            var response = new ApiResponse<PhotoDto>();
            var uploadResponse = await photoService.Upload(file);

            var photo = new Photo()
            {
                PublicId = uploadResponse.PublicId,
                Url = uploadResponse.Url.AbsoluteUri,
            };

            if (user?.Photos?.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);
            await unitOfWork.UserRepository.Update(user);
            response.Data = mapper.Map<PhotoDto>(photo);
            return response;
        }

        public async Task<ApiResponse<bool>> DeletePhoto(string username, string publicId)
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

            if (user != null)
            {
                var userPhoto = user.Photos.FirstOrDefault(x => x.Id == Id);

                if (userPhoto != null)
                {
                    foreach (var photo in user.Photos)
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

        public async Task<ApiResponse<DateTime>> LogUserActivity(string username)
        {
            var response = new ApiResponse<DateTime>();
            response.Data = await unitOfWork.UserRepository.LogUserActivity(username);
            return response;
        }


        /// <summary>
        /// //Likes 
        /// </summary>
        /// <param name="getLikesRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse<PagedList<LikeDto>>> GetUserLikes(GetLikesRequest getLikesRequest)
        {
            var response = new ApiResponse<PagedList<LikeDto>>();

            response.Data = await unitOfWork.LikeRepository.GetLikes(getLikesRequest);

            return response;
        }

        public async Task<ApiResponse<bool>> AddUserLike(string username, int LikedUserId)
        {
            var response = new ApiResponse<bool>();
            var user = await this.unitOfWork.UserRepository.GetByUserName(username);
            bool isLikeExist = await this.unitOfWork.LikeRepository.GetLike(user.Id, LikedUserId) != null;
            if (isLikeExist)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "you have already liked this user";
                return response;
            }
            var like = new Like()
            {
                LikedUserId = LikedUserId,
                SourceUserId = user.Id
            };

            user.UserLikes.Add(like);

            response.Data = await unitOfWork.UserRepository.Update(user) > 0;

            return response;
        }

        public async Task<ApiResponse<AppUser>> GetUserWithLikes(int userId)
        {
            var response = new ApiResponse<AppUser>();

            response.Data = await this.unitOfWork.LikeRepository.GetUserWithLikes(userId);

            return response;
        }

        public async Task<ApiResponse<bool>> DeleteLike(string username, int DeleteLikedUserId)
        {
            var response = new ApiResponse<bool>();
            var user = await unitOfWork.UserRepository.GetByUserName(username);
            var likeToDelete = user.UserLikes.Where(like => like.LikedUserId == DeleteLikedUserId).FirstOrDefault();
            user.UserLikes.Remove(likeToDelete);
            response.Data = await unitOfWork.UserRepository.Update(user) > 0;
            return response;
        }

        /// <summary>
        /// Messages
        /// </summary>
        /// <param name="messageDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResponse<MessageDto>> AddMessage(string username, CreateMessageDto messageDto)
        {
            var response = new ApiResponse<MessageDto>();

            var user = await unitOfWork.UserRepository.GetByUserName(username);
            var recipet = await unitOfWork.UserRepository.GetByUserName(messageDto.RecipientUsername);

            Message message = new Message();
            message.RecipientId = recipet.Id;
            message.Sender = user;
            message.SenderId = user.Id;
            message.Content = messageDto.Content;
            message.CreatedBy = user.UserName;
            message.RecipientUsername = recipet.UserName;
            message.Recipient = recipet;
            message.SenderUsername = user.UserName;
            message.CreationDate = DateTime.Now;

            response.Data = mapper.Map<MessageDto>(message);

            await unitOfWork.MessageRepository.Insert(message);

            return response;
        }

        public async Task<ApiResponse<IEnumerable<MessageDto>>> GetMessageThread(string recipeintUsername, string senderUsername)
        {
            var response = new ApiResponse<IEnumerable<MessageDto>>();

            var user = await this.unitOfWork.UserRepository.GetByUserName(recipeintUsername);

            var sender = await this.unitOfWork.UserRepository.GetByUserName(senderUsername);

            var messages = await this.unitOfWork.MessageRepository.GetMessageThread(sender.Id, user.Id);

            var unreadedMessages = messages.Where(message => message.DateReaded == null && message.RecipientUsername == recipeintUsername);

            if (unreadedMessages.Any())
            {
                foreach (var unreadedMessage in unreadedMessages)
                {
                    unreadedMessage.DateReaded = DateTime.Now;
                }
                await unitOfWork.SaveAsync();
            }

            response.Data = mapper.Map<List<MessageDto>>(messages.ToList());

            return response;

        }


        public async Task<ApiResponse<PagedList<MessageDto>>> GetUserMessages(GetUserMessagesRequest getUserMessagesRequest)
        {
            var response = new ApiResponse<PagedList<MessageDto>>();
            response.Data = await unitOfWork.MessageRepository.GetUserMessage(getUserMessagesRequest);
            return response;
        }
    }
}
