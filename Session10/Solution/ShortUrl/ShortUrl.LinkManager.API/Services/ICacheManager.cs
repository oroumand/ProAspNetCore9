namespace ShortUrl.LinkManager.API.Services;

public interface ICacheManager
{
    Task<string?> PopCodeAsync(int length, CancellationToken ct);
    Task PushCodesAsync(int length, IEnumerable<string> codes, CancellationToken ct);
    Task<long> PoolSizeAsync(int length, CancellationToken ct);
}
