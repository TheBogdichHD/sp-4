using Lab4.Models;
using Lab4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers;

[Route("subscribe")]
public class SubscribeController : Controller
{
    private readonly ICsvStorageService _csvStorageService;

    public SubscribeController(ICsvStorageService csvStorageService)
    {
        _csvStorageService = csvStorageService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Subscribe(EmailRequest data)
    {
        if (!ModelState.IsValid)
        {
            TempData["SubscribeError"] = "Please enter a valid email.";
            return RedirectBack();
        }

        await _csvStorageService.SaveSubscriptionAsync(data.Email);
        TempData["SubscribeSuccess"] = "1";
        return RedirectBack();
    }

    private IActionResult RedirectBack()
    {
        var referer = Request.Headers.Referer.ToString();
        if (string.IsNullOrWhiteSpace(referer))
        {
            return Redirect("/");
        }

        return Redirect(referer);
    }
}
