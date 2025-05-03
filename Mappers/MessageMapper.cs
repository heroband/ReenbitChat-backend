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
            Timestamp = message.Timestamp
        };
    }
}