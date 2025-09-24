using Microsoft.AspNetCore.Mvc;
using ShortUrl.LinkManager.API.DAL;
using ShortUrl.LinkManager.API.Dtos;

namespace ShortUrl.LinkManager.API.Controllers;
[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    private readonly ILinkRepository _repo;
    private readonly IClickEventRepository _clickRepo;

    public ReportsController(ILinkRepository repo, IClickEventRepository clickRepo)
    {
        _repo = repo;
        _clickRepo = clickRepo;
    }

    [HttpGet("summary")]
    public async Task<ActionResult<ReportSummaryResponse>> Summary(CancellationToken ct)
    {
        var (total, active, disabled, expired) = await _repo.SummaryAsync(ct);
        return Ok(new ReportSummaryResponse
        {
            TotalLinks = total,
            ActiveLinks = active,
            DisabledLinks = disabled,
            ExpiredLinks = expired
        });
    }

    [HttpGet("clicks/recent")]
    public async Task<ActionResult<IEnumerable<RecentClickDto>>> Recent([FromQuery] int take = 50, CancellationToken ct = default)
    {
        var items = await _clickRepo.GetRecentAsync(take, ct);
        var dto = items.Select(x => new RecentClickDto
        {
            Ts = x.Ts,
            Code = x.Code,
            Ip = x.Ip,
            Referrer = x.Referrer,
            UserAgent = x.UserAgent
        });
        return Ok(dto);
    }
}