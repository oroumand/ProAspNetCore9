using ShortUrl.LinkManager.API.DAL;
using StackExchange.Redis;

namespace ShortUrl.LinkManager.API.Services;

public class RedirectResolver : IRedirectResolver
{
    private readonly ILinkRepository _repo;
    private readonly IConnectionMultiplexer _redis;

    public RedirectResolver(ILinkRepository repo, IConnectionMultiplexer redis)
    {
        _repo = repo;
        _redis = redis;
    }

    private static string CacheKey(string code) => $"link:resolve:{code}";

    public async Task<(bool ok, bool gone, string? longUrl)> ResolveAsync(string code, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(code))
            return (false, false, null);

        var db = _redis.GetDatabase();

        // 1) کش: longUrl یا مارکر "GONE"/"NF"؟
        var cached = await db.StringGetAsync(CacheKey(code));
        if (cached.HasValue)
        {
            var val = (string)cached!;
            if (val == "::GONE") return (false, true, null);
            if (val == "::NF") return (false, false, null);
            return (true, false, val);
        }

        // 2) DB
        var link = await _repo.FindByCodeAsync(code, ct);
        if (link is null)
        {
            await db.StringSetAsync(CacheKey(code), "::NF", TimeSpan.FromMinutes(5));
            return (false, false, null);
        }

        // وضعیت/انقضا
        var now = DateTimeOffset.UtcNow;
        var expired = link.ExpiryDate.HasValue && link.ExpiryDate.Value <= now;
        var disabled = link.Status.ToString().Equals("Disabled", StringComparison.OrdinalIgnoreCase);
        if (expired)
        {
            await db.StringSetAsync(CacheKey(code), "::GONE", TimeSpan.FromMinutes(15));
            return (false, true, null);
        }
        if (disabled)
        {
            await db.StringSetAsync(CacheKey(code), "::NF", TimeSpan.FromMinutes(15));
            return (false, false, null);
        }

        // 3) کش کردن longUrl با TTL مناسب
        TimeSpan ttl = TimeSpan.FromHours(24);
        if (link.ExpiryDate is DateTimeOffset exp)
        {
            var left = exp - now;
            if (left > TimeSpan.Zero && left < ttl) ttl = left;
        }

        await db.StringSetAsync(CacheKey(code), link.LongUrl, ttl);
        return (true, false, link.LongUrl);
    }
}