using System.ComponentModel.DataAnnotations;

namespace open_ai_chat_domain.Entities;

public class ChatConfiguration
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(500)]
    public string Description { get; set; }
    
    [MaxLength(100)]
    public string Model { get; set; }
    
    public bool Stream { get; set; }
    
    public int MaxTokens { get; set; }
}