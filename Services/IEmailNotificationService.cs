using Lab4.Models;

namespace Lab4.Services;

public interface IEmailNotificationService
{
    Task SendSubscriptionAsync(string subscriberEmail);
    Task SendContactAsync(ContactRequest request);
}
