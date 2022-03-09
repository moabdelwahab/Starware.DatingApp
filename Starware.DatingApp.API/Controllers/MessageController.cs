using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Starware.DatingApp.API.Extensions;
using Starware.DatingApp.Core.DTOs;
using Starware.DatingApp.Core.Requests;
using Starware.DatingApp.Core.ServiceContracts;

namespace Starware.DatingApp.API.Controllers
{
    [Authorize]
    public class MessageController : BaseApiController
    {
        private readonly IUserService userservice;

        public MessageController(IUserService userservice)
        {
            this.userservice = userservice;
        }

        [HttpPost]
        [Route("add-message")]
        public async Task<ActionResult> AddMessage([FromBody]CreateMessageDto createMessageDto)
        {
            var senderUsername = User.GetUserName();
            return Ok(await this.userservice.AddMessage(senderUsername, createMessageDto));
        }

        [HttpGet]
        [Route("get-thread")]
        public async Task<ActionResult> GetMessageThread([FromQuery]string senderUsername)
        {
            var username= User.GetUserName();
            return Ok(await this.userservice.GetMessageThread(username, senderUsername));
        }

        [HttpGet]
        [Route("get-messages")]
        public async Task<ActionResult> GetMessages([FromQuery]GetUserMessagesRequest getUserMessagesRequest)
        {
            var username = User.GetUserName();
            getUserMessagesRequest.Username = username;
            var response =await userservice.GetUserMessages(getUserMessagesRequest);
            Response.AddPaginationHeader(response.Data.TotalPages, response.Data.TotalCount, response.Data.PageSize, response.Data.CurrentPage);
            return Ok(response);
        }

    }
}
