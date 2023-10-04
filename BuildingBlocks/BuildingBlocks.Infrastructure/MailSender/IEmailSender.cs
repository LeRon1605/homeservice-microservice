namespace BuildingBlocks.Infrastructure.MailSender;

public interface IEmailSender
{
    void SendEmail(Message message);
}