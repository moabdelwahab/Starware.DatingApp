using Microsoft.AspNetCore.SignalR;
using Starware.DatingApp.API.Extensions;
using Starware.DatingApp.Core.ServiceContracts;

namespace Starware.DatingApp.API.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IUserService userService;

        public MessageHub(IUserService userService)
        {
            this.userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"].ToString();
            var groupName = GetGroupName(Context.User.GetUserName(), otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var messages = await this.userService.GetMessageThread(Context.User.GetUserName(), otherUser);
            await Clients.Group(groupName).SendAsync("ReciveMessageThread",messages);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }


        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller};
        }
    }
}
