using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace backend.Services;

public class ChatHub : Hub
{
    private readonly IMessageRepository _messageRepository;

    public ChatHub(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    public async Task JoinChat(string username)
    {
        await Clients.All.SendAsync("ReceiveMessage", "Admin", $"{username} приєднався до чату");
    }
    public async Task SendMessage(string user, string message)
    {
        var msg = new Message
        {
            User = user,
            Text = message
        };

        await _messageRepository.AddAsync(msg);

        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    
}