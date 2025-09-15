namespace UserAndCommentsManager.Events;

public record NewCommentEvent(Guid Id, string Text, Guid AuthorId, DateTime CreatedAt);
