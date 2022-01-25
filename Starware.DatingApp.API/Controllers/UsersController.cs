using Microsoft.AspNetCore.Mvc;

namespace Starware.DatingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        public int MyProperty { get; set; }
    }
}
