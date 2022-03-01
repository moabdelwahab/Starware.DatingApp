using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Starware.DatingApp.API.Extensions;
using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.Core.InfrastructureContracts;
using Starware.DatingApp.Core.Requests;
using Starware.DatingApp.Core.ServiceContracts;
using Starware.DatingApp.SharedKernal.Common;
using System.Security.Claims;

namespace Starware.DatingApp.API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService userService;
        private readonly IPhotoService photoService;

        public UsersController(IUserService userService,
                                IPhotoService photoService)
        {
            this.userService = userService;
            this.photoService = photoService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ApiResponse<List<MemberDto>>>> GetAllUsers([FromQuery]GetAllUsersRequest getAllUsersRequest)
        {
            var username = User.GetUserName();

            var user = await this.userService.GetMemberByUsername(username);

            if(user.Data.Gender.ToLower() == "male")
            {
                getAllUsersRequest.Gender = "female";
            }else
            {
                getAllUsersRequest.Gender = "male";
            }
            var usersReponse = await this.userService.GetAllUser(getAllUsersRequest);

            Response.AddPaginationHeader(usersReponse.Data.TotalPages, usersReponse.Data.TotalCount, usersReponse.Data.PageSize, usersReponse.Data.CurrentPage);
            return Ok(new ApiResponse<List<MemberDto>>()
            {
                Data = usersReponse.Data.ToList(),
                StatusCode = System.Net.HttpStatusCode.OK,
                Message= ""
            });
        }

        [HttpGet]
        [Route("GetUserByUsername/{username}")]
        public async Task<ActionResult<ApiResponse<MemberDto>>> GetuserByUsername(string username)
        {
            return Ok(await this.userService.GetMemberByUsername(username));
        }

        [HttpPut]
        [Route("updateUser")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateUser([FromBody] MemberDto member)
        {
            return Ok(await this.userService.UpdateUser(member));
        }

        [HttpPost]
        [Route("add-photo")]
        public async Task<ActionResult<ApiResponse<PhotoDto>>> addPhoto([FromForm] IFormFile file)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(await this.userService.AddPhoto(file, username));
        }

        [HttpDelete]
        [Route("delete-photo/{publicId}")]
        public async Task<ActionResult<ApiResponse<PhotoDto>>> deletePhoto([FromRoute] string publicId)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(await this.userService.DeletePhoto(username,publicId)); ;
        }


        [HttpPut]
        [Route("set-main-photo/{photoId}")]
        public async Task<ActionResult<ApiResponse<bool>>> SetMainPhoto(int photoId)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = await this.userService.SetMainPhoto(username, photoId);
            return Ok(response);
        }

    }
}
