using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Starware.DatingApp.API.Extensions;

namespace Starware.DatingApp.API.SignalR
{
    [Authorize]
    public class PresenceHub : Hub
    {
        public PresenceHub(PresenceTracker _tracker)
        {
            Tracker = _tracker;
        }

        public PresenceTracker Tracker { get; }

        public override async Task OnConnectedAsync()
        {
            Tracker.UserConnected(Context.User.GetUserName(), Context.ConnectionId);
            await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUserName());
            var currentUsers = await  Tracker.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers", currentUsers);

        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Tracker.UserDisconnected(Context.User.GetUserName(), Context.ConnectionId);
            await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUserName());
            await base.OnDisconnectedAsync(exception);
            var currentUsers = await Tracker.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers", currentUsers);
        }
    }
}
