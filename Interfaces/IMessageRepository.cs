using backend.Models;

namespace backend.Interfaces;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetAllAsync();
    Task<Message> AddAsync(Message message);
}