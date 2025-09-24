using Microsoft.AspNetCore.Mvc.RazorPages;
using ShortUrl.RazorShortener.Models;
using ShortUrl.RazorShortener.Services;

namespace ShortUrl.RazorShortener.Pages;

public class ReportsModel : PageModel
{
    private readonly ILinkApiClient _api;
    public ReportsModel(ILinkApiClient api) => _api = api;

    public ReportSummaryResponse Summary { get; private set; } = new();

    public async Task OnGet(CancellationToken ct)
    {
        Summary = await _api.GetSummaryAsync(ct);
    }
}
