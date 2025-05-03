using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public class CreateMessageDto
{
    [Required]
    public string User { get; set; }

    [Required]
    public string Text { get; set; }
}