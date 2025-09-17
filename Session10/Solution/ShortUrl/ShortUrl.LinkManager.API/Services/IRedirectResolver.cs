namespace ShortUrl.LinkManager.API.Services;

public interface IRedirectResolver
{
    Task<(bool ok, bool gone, string? longUrl)> ResolveAsync(string code, CancellationToken ct);
}