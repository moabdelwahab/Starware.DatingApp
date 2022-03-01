using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Starware.DatingApp.API.Extensions;
using Starware.DatingApp.Core.Requests;
using Starware.DatingApp.Core.ServiceContracts;
using Starware.DatingApp.SharedKernal.Common;

namespace Starware.DatingApp.API.Controllers
{
    [Authorize]
    public class LikeController : BaseApiController
    {
        private readonly IUserService userService;

        public LikeController(IUserService userService )
        {
            this.userService = userService;
        }
        

        [Route("add-user-like")]
        [HttpPost]
        public async Task<ActionResult> AddUserLike([FromBody]int likedUserId)
        {
            var username = User.GetUserName();

            var response = await userService.AddUserLike(username, likedUserId);

            if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(response.Message);
            }
            
            return Ok(response);

        }

        [Route("get-user-with-likes")]
        [HttpGet]
        public async Task<ActionResult> GetUserWithLikes()
        {
            var userId = User.GetUserId();
            var data = await userService.GetUserWithLikes(userId);
            return Ok(data);
        }

        [Route("get-user-likes")]
        [HttpGet]
        public async Task<ActionResult> GetUserLikes([FromQuery]GetLikesRequest getLikesRequest)
        {
            getLikesRequest.UserId = User.GetUserId();
            var data = await userService.GetUserLikes(getLikesRequest);
            Response.AddPaginationHeader(data.Data.TotalPages, data.Data.TotalCount, data.Data.PageSize, data.Data.CurrentPage);
            return Ok(data);
        }

        [Route("delete-like")]
        [HttpDelete]
        public async Task<ActionResult> DeleteLike([FromQuery] int likeUserId)
        {
            var username = User.GetUserName();
            return Ok(await userService.DeleteLike(username, likeUserId));
        }


    }
}
