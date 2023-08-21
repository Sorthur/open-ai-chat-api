using System.ComponentModel.DataAnnotations;

namespace open_ai_chat_domain.Entities;

public class User
{
    public Guid Id { get; set; }
    
    [MaxLength(100)]
    public string Login { get; set; }
    
    [MaxLength(100)]
    public string Password { get; set; }
    
    [MaxLength(200)]
    public string ApiKey { get; set; }

    public ICollection<ChatConfiguration> ChatConfigurations { get; set; }
    
    public ICollection<Conversation> Conversations { get; set; }
}