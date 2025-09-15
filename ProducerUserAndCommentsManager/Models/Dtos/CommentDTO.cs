using System.ComponentModel.DataAnnotations;

namespace UserAndCommentsManager.Models.Dtos;

public class CommentDTO
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required, StringLength(140)]
    public string Message { get; set; }
}
