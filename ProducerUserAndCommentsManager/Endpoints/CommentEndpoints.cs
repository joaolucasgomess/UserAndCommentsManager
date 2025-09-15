using MiniValidation;
using UserAndCommentsManager.Data;
using UserAndCommentsManager.Messaging.Abstracts;
using UserAndCommentsManager.Models;
using UserAndCommentsManager.Models.Dtos;

namespace UserAndCommentsManager.Endpoints;

public static class CommentEndpoints
{
    public static WebApplication MapCommentsEndpoint(this WebApplication app)
    {
        app.MapPost("api/comments/", async (CommentDTO createCommentDto, MinimalContextDb db) =>
        {
            if (!MiniValidator.TryValidate(createCommentDto, out var errors))
                return Results.ValidationProblem(errors);

            var comment = new Comment(createCommentDto.UserId, createCommentDto.Message);
            
            db.Add(comment);
            var result = await db.SaveChangesAsync();
            
            return result > 0
                ? Results.Created()
                : Results.BadRequest();
        });
        
        return app;
    }
}
