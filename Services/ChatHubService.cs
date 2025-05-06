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
        try
        {
            var messageEntity = dto.ToEntity();
            messageEntity.Sentiment = await _sentimentService.AnalyzeSentimentAsync(messageEntity.Text);
            await _messageRepository.AddAsync(messageEntity);
            await Clients.All.SendAsync("ReceiveMessage", messageEntity.ToDto());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка в SendMessage: {ex.Message}");
            throw;
        }
    }
}