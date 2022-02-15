using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Starware.DatingApp.Core.Domains;

namespace Starware.DatingApp.API.Controllers
{
    public class BuggyController : BaseApiController
    {

        private readonly List<AppUser> users = new List<AppUser>();

        [Authorize]
        [HttpGet]
        [Route("auth")]
        public ActionResult<string> GetUsername()
        {
            return Ok("Mohaemd");
        }


        [Route("not-found")]
        public ActionResult<string> GetNotFound()
        {
            var thing = users.FirstOrDefault( x=> x.UserName== "asdasdasd");
            if (thing == null)
            {
                return NotFound(thing);
            }
            return Ok();
        }


        [Route("server-error")]
        public ActionResult<string> GetServerError()
        {
            throw new NotImplementedException();
        }


        [Route("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("Mohaemd");
        }
    }
}
