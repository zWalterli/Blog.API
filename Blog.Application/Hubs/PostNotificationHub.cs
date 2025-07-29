using Microsoft.AspNetCore.SignalR;

namespace Blog.Application.Hubs;

public class PostNotificationHub : Hub
{
    public async Task SendNotification(int postId, string message)
    {
        await Clients.All.SendAsync("NewPost", postId, message);
    }
}