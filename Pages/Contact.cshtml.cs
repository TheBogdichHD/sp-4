using Lab4.Models;
using Lab4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab4.Pages;

public class ContactModel : PageModel
{
    private readonly ICsvStorageService _csvStorageService;

    public ContactModel(ICsvStorageService csvStorageService)
    {
        _csvStorageService = csvStorageService;
    }

    [BindProperty]
    public ContactRequest Input { get; set; } = new();

    public bool IsSubmitted { get; private set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _csvStorageService.SaveContactAsync(Input);
        IsSubmitted = true;
        Input = new ContactRequest();
        ModelState.Clear();

        return Page();
    }
}
