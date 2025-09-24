using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShortUrl.RazorShortener.Models;
using ShortUrl.RazorShortener.Services;

namespace ShortUrl.RazorShortener.Pages;

public class IndexModel : PageModel
{
    private readonly ILinkApiClient _api;
    public IndexModel(ILinkApiClient api) => _api = api;

    [BindProperty] public CreateLinkViewModel Input { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid) return Page();

        try
        {
            var req = new CreateLinkRequest
            {
                LongUrl = Input.LongUrl,
                ExpiryDate = Input.ExpiryDate,
                MaxClicks = Input.MaxClicks,
                CodeLength = Input.CodeLength
            };

            var resp = await _api.CreateLinkAsync(req, ct);
            Input.ShortUrl = resp.ShortUrl;
            ModelState.Clear();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"خطا در ساخت لینک: {ex.Message}");
        }

        return Page();
    }
}
