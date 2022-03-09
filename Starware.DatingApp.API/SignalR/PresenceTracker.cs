namespace Starware.DatingApp.API.SignalR
{
    public class PresenceTracker
    {
        private static readonly Dictionary<string, List<string>> onlineUsers = new Dictionary<string, List<string>>();

        public Task UserConnected(string username, string connectionId)
        {
            lock (onlineUsers)
            {
                if (onlineUsers.ContainsKey(username))
                {
                    onlineUsers[username].Add(connectionId);
                }
                else
                {
                    onlineUsers.Add(username, new List<string>() { connectionId });
                }
            }
            return Task.CompletedTask;
        }

        public Task UserDisconnected(string username, string ConnectionId)
        {
            lock (onlineUsers)
                if (!onlineUsers.ContainsKey(username)) return Task.CompletedTask;
            onlineUsers[username].Remove(ConnectionId);
            if (onlineUsers[username].Count == 0)
            {
                onlineUsers.Remove(username);
            }
            return Task.CompletedTask;
        }

        public Task<string[]> GetOnlineUsers()
        {
            string[] onlineUsersArray;
            lock (onlineUsers)
            {
                onlineUsersArray = onlineUsers.OrderBy(k => k.Key).Select(k => k.Key).ToArray(); 
            }
            return Task.FromResult(onlineUsersArray);
        }
    }
}
