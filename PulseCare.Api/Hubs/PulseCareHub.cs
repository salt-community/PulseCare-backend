using Microsoft.AspNetCore.SignalR;

public class PulseCareHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }

    public async Task JoinConversation(Guid conversationId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
    }

    public async Task LeaveConversation(Guid conversationId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId.ToString());
    }

    public async Task MarkAsRead(Guid conversationId, Guid messageId)
    {
        await Clients.Group(conversationId.ToString())
            .SendAsync("MessageRead", messageId);
    }

    public async Task Typing(Guid conversationId)
    {
        await Clients.Group(conversationId.ToString())
            .SendAsync("Typing", Context.UserIdentifier);
    }
}
