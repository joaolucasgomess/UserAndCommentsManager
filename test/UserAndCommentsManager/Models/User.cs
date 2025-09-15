using System.ComponentModel.DataAnnotations;

namespace UserAndCommentsManager.Models;

public class User
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}
