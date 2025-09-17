using StackExchange.Redis;

namespace ShortUrl.LinkManager.API.Services;

public class CacheManager : ICacheManager
{
    private readonly IConnectionMultiplexer _mux;
    public CacheManager(IConnectionMultiplexer mux) => _mux = mux;

    private static string PoolKey(int length) => $"codes:pool:{length}";

    public async Task<string?> PopCodeAsync(int length, CancellationToken ct)
    {
        var db = _mux.GetDatabase();
        return await db.ListRightPopAsync(PoolKey(length));
    }

    public async Task PushCodesAsync(int length, IEnumerable<string> codes, CancellationToken ct)
    {
        var db = _mux.GetDatabase();
        var key = PoolKey(length);
        var values = codes.Select(x => (RedisValue)x).ToArray();
        if (values.Length > 0)
            await db.ListLeftPushAsync(key, values);
    }

    public async Task<long> PoolSizeAsync(int length, CancellationToken ct)
    {
        var db = _mux.GetDatabase();
        return await db.ListLengthAsync(PoolKey(length));
    }
}
