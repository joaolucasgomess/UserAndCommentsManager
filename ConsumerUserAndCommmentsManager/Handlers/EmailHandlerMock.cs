using ConsumerUserAndCommmentsManager.Handlers.Abstracts;

namespace ConsumerUserAndCommmentsManager.Handlers;

public class EmailHandlerMock : IEmailSender
{
    public Task SendEmailAsync(string emailTo, string subject, string message)
    {
        Console.WriteLine("Sending email to: ", emailTo);

        return Task.CompletedTask;
    }
}
