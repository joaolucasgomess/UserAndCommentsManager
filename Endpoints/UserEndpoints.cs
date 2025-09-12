using MiniValidation;
using UserAndCommentsManager.Data;
using UserAndCommentsManager.Models.Dtos;

namespace UserAndCommentsManager.Endpoints;

public static class UserEndpoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("api/users/",async (UserDTO user, MinimalContextDb db) =>
        {
            if (!MiniValidator.TryValidate(user, out var errors))
                return Results.ValidationProblem(errors);;
            
            db.Add(user);
            var result = await db.SaveChangesAsync();
            
            return result > 0
                ? Results.Created()
                : Results.BadRequest();
        });
        
        return app;
    }
}
