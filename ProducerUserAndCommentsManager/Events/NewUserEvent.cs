namespace UserAndCommentsManager.Events;

public record NewUserEvent(Guid Id, string Username, string Email, DateTime CreatedAt);