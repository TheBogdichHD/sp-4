using Lab4.Models;

namespace Lab4.Services;

public interface ICsvStorageService
{
    Task SaveSubscriptionAsync(string email);
    Task SaveContactAsync(ContactRequest request);
}
