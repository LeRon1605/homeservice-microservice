using BuildingBlocks.Application.MailSender;
using BuildingBlocks.Infrastructure.MailSender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Presentation.MailSender;

public static class EmailExtension
{
    public static IServiceCollection AddEmailSetting(this IServiceCollection services,IConfiguration configuration)
    {
        var emailConfig = configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();
        services.AddSingleton(emailConfig);
        services.AddScoped<IEmailSender, EmailSender>();
        return services;
    }
}