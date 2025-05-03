using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessageRepository _messageRepository;

    public MessagesController(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    [HttpPost]
    public async Task<IActionResult> PostMessage([FromBody] Message message)
    {
        var savedMessage = await _messageRepository.AddAsync(message);
        return Ok(savedMessage);
    }

    [HttpGet]
    public async Task<IEnumerable<Message>> GetMessages()
    {
        return await _messageRepository.GetAllAsync();
    }
}