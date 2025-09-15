using System.Net;
using System.Net.Mail;
using ConsumerUserAndCommmentsManager.Handlers.Abstracts;

namespace ConsumerUserAndCommmentsManager.Handlers;

public class EmailHandler : IEmailSender
{
    private readonly string _host;
    private readonly string _userName;
    private readonly string _password;

    public EmailHandler()
    {
        _host = "smtp.gmail.com";
        _userName = "";
        _password = "";
    }
    public Task SendEmailAsync(string emailTo, string subject, string message)
    {
        var mail = new MailMessage();
        mail.From = new MailAddress(_userName);
        mail.To.Add(new MailAddress(emailTo));
        mail.Subject = subject;
        mail.Body = message;
        mail.IsBodyHtml = true;

        var smtp = new SmtpClient();
        smtp.Host = _host;
        smtp.Port = 465;
        smtp.EnableSsl = true;
        smtp.Credentials = new NetworkCredential(_userName, _password);

        try
        {
            smtp.Send(mail);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        
        return Task.CompletedTask;
    }
}
