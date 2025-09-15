using System.ComponentModel.DataAnnotations;

namespace UserAndCommentsManager.Models.Dtos;

public class UserDTO
{
    [Required]
    public string UserName { get; set; }
    
    [EmailAddress, Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}
