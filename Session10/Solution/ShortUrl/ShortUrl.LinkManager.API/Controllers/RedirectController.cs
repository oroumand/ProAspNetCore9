using Microsoft.AspNetCore.Mvc;
using ShortUrl.LinkManager.API.DAL;
using ShortUrl.LinkManager.API.Services;

namespace ShortUrl.LinkManager.API.Controllers;

[ApiController]
[Route("")]
public class RedirectController : ControllerBase
{
    private readonly IRedirectResolver _resolver;
    private readonly ILinkRepository _links;
    private readonly IClickLogger _clicks;

    public RedirectController(IRedirectResolver resolver, ILinkRepository links, IClickLogger clicks)
    {
        _resolver = resolver;
        _links = links;
        _clicks = clicks;
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> Go([FromRoute] string code, [FromQuery] bool permanent = false, CancellationToken ct = default)
    {
        var (ok, gone, url) = await _resolver.ResolveAsync(code, ct);

        if (ok && !string.IsNullOrWhiteSpace(url))
        {
            // 🔹 آمار: برای داشتن LinkId، یک lookup سریع روی DB انجام می‌دهیم
            var link = await _links.FindByCodeAsync(code, ct);
            if (link is not null)
            {
                // fire-and-forget (عدم بلوکه‌کردن مسیر داغ ریدایرکت)
                _ = _clicks.LogAsync(link.Id, link.Code, link.LongUrl, HttpContext, CancellationToken.None);
            }

            return permanent ? RedirectPermanent(url) : Redirect(url);
        }

        if (gone) return StatusCode(StatusCodes.Status410Gone);
        return NotFound();
    }
}