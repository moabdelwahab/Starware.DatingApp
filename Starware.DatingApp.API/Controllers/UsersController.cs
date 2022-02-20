using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.Core.ServiceContracts;

namespace Starware.DatingApp.API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAllUsers()
        {
            return  Ok(await this.userService.GetAllUser());
        }

        [HttpGet]
        [Route("GetUserByUsername/{username}")]
        public async Task<ActionResult>  GetuserByUsername(string username)
        {
            return Ok( await this.userService.GetMemberByUsername(username));
        }

        [HttpPut]
        [Route("updateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] MemberDto member)
        {
            return Ok(await this.userService.UpdateUser(member));
        }

    }
}
