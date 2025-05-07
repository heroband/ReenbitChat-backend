using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

/// <summary>
/// Repository for accessing messages from the database
/// </summary>
public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _context;

    public MessageRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Getting last N messages in chronological order
    /// </summary>
    /// <param name="count">The number of recent messages to get</param>
    /// <returns>Received messages</returns>
    public async Task<IEnumerable<Message>> GetLastMessagesAsync(int count)
    {
        return await _context.Messages
            .OrderByDescending(m => m.Timestamp)
            .Take(count)
            .OrderBy(m => m.Timestamp)
            .ToListAsync();
    }

    /// <summary>
    /// Adds a new message to the database
    /// </summary>
    /// <param name="message">The message entity to add</param>
    /// <returns>The saved message entity</returns>
    public async Task<Message> AddAsync(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }
}