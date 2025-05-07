using backend.Dtos;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.SignalR;

namespace backend.Services;

/// <summary>
/// SignalR Hub for real-time chat communication.
/// </summary>
public class ChatHubService : Hub
{
    private readonly IMessageRepository _messageRepository;
    private readonly SentimentService _sentimentService;

    public ChatHubService(IMessageRepository messageRepository, SentimentService sentimentService)
    {
        _messageRepository = messageRepository;
        _sentimentService = sentimentService;
    }
    
    /// <summary>
    /// Notifies all clients that a user has joined the chat
    /// </summary>
    /// <param name="username">The name of the user who joined</param>
    public async Task JoinChat(string username)
    {
        var systemMessage = MessageMapper.CreateSystemMessage($"{username} приєднався до чату");
        await Clients.All.SendAsync("ReceiveMessage", systemMessage);
    }
    
    /// <summary>
    /// Processes a user message: analyzes sentiment,
    /// saves it to the database, and sends it to all clients back
    /// </summary>
    /// <param name="dto">DTO containing the message text and name of user who typed it</param>
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