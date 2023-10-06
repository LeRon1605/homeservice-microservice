namespace BuildingBlocks.Application.MailSender;

public interface IEmailSender
{
    void SendEmail(Message message);
}