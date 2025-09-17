using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortUrl.CodeGenerator.API.Dtos;
using ShortUrl.CodeGenerator.API.Services;

namespace ShortUrl.CodeGenerator.API.Controllers;
[Route("api/[controller]")]
[ApiController]

public class CodeController : ControllerBase
{
    private readonly ICodeGenerationService _svc;

    public CodeController(ICodeGenerationService svc) => _svc = svc;

    [HttpPost]
    public async Task<ActionResult<GenerateCodesResponse>> Generate([FromBody] GenerateCodesRequest req, CancellationToken ct)
    {
        var resp = await _svc.GenerateBatchAsync(req, ct);
        return Ok(resp);
    }

    [HttpGet("status")]
    public async Task<ActionResult<InventoryStatusResponse>> Status([FromQuery] int length = 7, CancellationToken ct = default)
    {
        var resp = await _svc.GetInventoryAsync(length, ct);
        return Ok(resp);
    }
}
