namespace ConsumerUserAndCommmentsManager.Handlers.Abstracts;

public interface IEmailSender
{
    Task SendEmailAsync(string emailTo, string subject, string message);
}
