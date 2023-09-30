using System.ComponentModel.DataAnnotations;

namespace open_ai_chat_domain.Entities;

public class Conversation
{
    public Guid Id { get; set; }
    
    [MaxLength(100)]
    public string ConversationKey { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
}