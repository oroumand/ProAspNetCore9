using ShortUrl.LinkManager.API.DAL;
using ShortUrl.LinkManager.API.Domain;

namespace ShortUrl.LinkManager.API.Services;

public class ClickLogger : IClickLogger
{
    private readonly IClickEventRepository _repo;
    public ClickLogger(IClickEventRepository repo) => _repo = repo;

    public async Task LogAsync(Guid linkId, string code, string longUrl, HttpContext http, CancellationToken ct)
    {
        // استخراج IP (X-Forwarded-For → RemoteIp)
        var ip = http.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                 ?? http.Connection.RemoteIpAddress?.ToString();

        var ua = http.Request.Headers.UserAgent.ToString();
        var referer = http.Request.Headers.Referer.ToString();

        var ev = new ClickEvent
        {
            LinkId = linkId,
            Code = code,
            Ts = DateTimeOffset.UtcNow,
            Ip = ip,
            UserAgent = ua,
            Referrer = referer
        };

        await _repo.AddAsync(ev, ct);
    }
}