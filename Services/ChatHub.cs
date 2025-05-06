using backend.Dtos;
using backend.Interfaces;
using backend.Mappers;
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
        var systemMessage = MessageMapperExtensions.CreateSystemMessage($"{username} приєднався до чату");
        await Clients.All.SendAsync("ReceiveMessage", systemMessage);
    }
    public async Task SendMessage(CreateMessageDto dto)
    {
        var messageEntity = await _messageRepository.AddAsync(dto.ToEntity());
        await Clients.All.SendAsync("ReceiveMessage", messageEntity.ToDto());
    }
}