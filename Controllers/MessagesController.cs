using backend.Dtos;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessageRepository _messageRepository;

    public MessagesController(IMessageRepository messageRepository,  IHubContext<ChatHub> hubContext)
    {
        _messageRepository = messageRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetLastMessages()
    {
        var messages = await _messageRepository.GetLastMessagesAsync(10);
        return Ok(messages.Select(m => m.ToDto()));
    }
}