using BuildingBlocks.Application.MailSender;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace BuildingBlocks.Infrastructure.MailSender;

public class EmailSender : IEmailSender
{
    private readonly EmailConfiguration _emailConfig;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(EmailConfiguration emailConfig, ILogger<EmailSender> logger)
    {
        _emailConfig = emailConfig;
        _logger = logger;
    }
    
    public void SendEmail(Message message)
    {
        var emailMessage = CreateEmailMessage(message);

        Send(emailMessage);
    }
    
    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
        emailMessage.To.AddRange(message.To.Select(x => new MailboxAddress(string.Empty, x)));
        emailMessage.Subject = message.Subject;
        //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
        var builder = new BodyBuilder();
        builder.HtmlBody = message.Content;
        emailMessage.Body = builder.ToMessageBody ();
        
        
        return emailMessage;
    }

    private void Send(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                client.Send(mailMessage);
            }
            catch
            {
                _logger.LogError("Send mail error");
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}