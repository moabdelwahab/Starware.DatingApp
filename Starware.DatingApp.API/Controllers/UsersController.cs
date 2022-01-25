using Microsoft.AspNetCore.Mvc;
using Starware.DatingApp.Core.ServiceContracts;

namespace Starware.DatingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAllUsers()
        {
            return Ok(this.userService.GetAllUser());
        }
    }
}
