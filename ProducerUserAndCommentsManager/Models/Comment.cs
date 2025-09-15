using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAndCommentsManager.Models;

public class Comment
{
    [Required]
    public Guid Id { get; private set; }

    [Required]
    [ForeignKey("Users")]
    public Guid UserId { get; private set; }
    
    public User User { get; private set; }
    
    [Required, StringLength(140)]
    public string Message { get; private set; }
    
    [Required]
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    
    public DateTime? UpdatedAt { get; set; }

    public Comment(Guid userId, string message)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Message = message;
    }
}
