using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public class MessageDto
{
    public string User { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }
    public string MessageType { get; set; }
}