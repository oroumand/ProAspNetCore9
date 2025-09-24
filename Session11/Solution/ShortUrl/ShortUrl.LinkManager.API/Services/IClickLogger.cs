namespace ShortUrl.LinkManager.API.Services;

public interface IClickLogger
{
    Task LogAsync(Guid linkId, string code, string longUrl, HttpContext http, CancellationToken ct);
}