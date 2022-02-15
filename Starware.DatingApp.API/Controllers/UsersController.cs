using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Starware.DatingApp.Core.ServiceContracts;

namespace Starware.DatingApp.API.Controllers
{
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

        [Authorize]
        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<ActionResult>  GetUserById(int id)
        {
            return Ok( await this.userService.GetById(id));
        }
    }
}
