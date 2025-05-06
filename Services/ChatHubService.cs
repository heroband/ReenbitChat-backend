using backend.Dtos;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.SignalR;

namespace backend.Services;

public class ChatHubService : Hub
{
    private readonly IMessageRepository _messageRepository;
    private readonly SentimentService _sentimentService;

    public ChatHubService(IMessageRepository messageRepository, SentimentService sentimentService)
    {
        _messageRepository = messageRepository;
        _sentimentService = sentimentService;
    }
    public async Task JoinChat(string username)
    {
        var systemMessage = MessageMapper.CreateSystemMessage($"{username} приєднався до чату");
        await Clients.All.SendAsync("ReceiveMessage", systemMessage);
    }
    public async Task SendMessage(CreateMessageDto dto)
    {
        var messageEntity = await _messageRepository.AddAsync(dto.ToEntity());
        messageEntity.Sentiment = await _sentimentService.AnalyzeSentimentAsync(messageEntity.Text);
        await Clients.All.SendAsync("ReceiveMessage", messageEntity.ToDto());
    }
}