using Microsoft.AspNetCore.Mvc;
using Starware.DatingApp.API.ActionFilters;

namespace Starware.DatingApp.API.Controllers
{

    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[Controller]")]
    public class BaseApiController : ControllerBase
    {

    }
}
