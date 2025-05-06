using backend.Models;

namespace backend.Interfaces;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetLastMessagesAsync(int count);
    Task<Message> AddAsync(Message message);
}