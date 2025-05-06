using backend.Dtos;
using backend.Models;

namespace backend.Mappers;

public static class MessageMapperExtensions
{
    public static Message ToEntity(this CreateMessageDto dto)
    {
        return new Message
        {
            User = dto.User,
            Text = dto.Text,
            Timestamp = DateTime.UtcNow
        };
    }

    public static MessageDto ToDto(this Message message)
    {
        return new MessageDto
        {
            User = message.User,
            Text = message.Text,
            Sentiment = message.Sentiment,
            Timestamp = message.Timestamp,
            MessageType = "user"
        };
    }

    public static MessageDto CreateSystemMessage(string text)
    {
        return new MessageDto
        {
            User = "System",
            Text = text,
            Sentiment = null,
            Timestamp = DateTime.UtcNow,
            MessageType = "system"
        };
    }
}