using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortUrl.LinkManager.API.Dtos;
using ShortUrl.LinkManager.API.Services;

namespace ShortUrl.LinkManager.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LinksController : ControllerBase
{
    private readonly ILinkService _svc;
    public LinksController(ILinkService svc) => _svc = svc;

    [HttpPost]
    public async Task<ActionResult<CreateLinkResponse>> Create([FromBody] CreateLinkRequest req, CancellationToken ct)
    {
        var resp = await _svc.CreateAsync(req, ct);
        return Ok(resp);
    }
}
