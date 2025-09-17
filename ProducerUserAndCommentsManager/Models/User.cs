using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UserAndCommentsManager.Models;

[Index(nameof(UserName) ,IsUnique = true)]
[Index(nameof(Email) ,IsUnique = true)]
public class User
{
    [Required]
    public Guid Id { get; private set; }

    [Required]
    public string UserName { get; private set; }

    [EmailAddress]
    public string Email { get; private set; }

    [Required]
    public string Password { get; private set; }

    [Required]
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }

    public User(string userName, string email, string password)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Email = email;
        Password = password;
    }
}
