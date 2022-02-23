using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.Core.InfrastructureContracts;
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
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllUsers()
        {
            return Ok(await this.userService.GetAllUser());
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
