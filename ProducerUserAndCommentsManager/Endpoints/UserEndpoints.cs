using MiniValidation;
using UserAndCommentsManager.Data;
using UserAndCommentsManager.Events;
using UserAndCommentsManager.Messaging.Abstracts;
using UserAndCommentsManager.Models;
using UserAndCommentsManager.Models.Dtos;

namespace UserAndCommentsManager.Endpoints;

public static class UserEndpoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("api/users/",async (
            UserDTO registerUserDto, MinimalContextDb db, IMessageBusService busService) =>
        {
            if (!MiniValidator.TryValidate(registerUserDto, out var errors))
                return Results.ValidationProblem(errors);
            
            var user = new User(registerUserDto.UserName, registerUserDto.Email,  registerUserDto.Password);
            
            db.Add(user);
            var result = await db.SaveChangesAsync();

            if (result == 0)
                return Results.BadRequest();
            
            var @event = new NewUserEvent(user.Id, user.UserName, user.Email, user.CreatedAt);

            await busService.Publish<NewUserEvent>(@event, "new-user-event");

            return Results.Created();
        });
        
        return app;
    }
}
