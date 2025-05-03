using backend.Dtos;
using backend.Interfaces;
using backend.Mappers;
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
    public async Task<IActionResult> PostMessage([FromBody] CreateMessageDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var savedMessage = await _messageRepository.AddAsync(dto.ToEntity());
        return Ok(savedMessage.ToDto());
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
    {
        var messages = await _messageRepository.GetAllAsync();
        return Ok(messages.Select(m => m.ToDto()));
    }
}