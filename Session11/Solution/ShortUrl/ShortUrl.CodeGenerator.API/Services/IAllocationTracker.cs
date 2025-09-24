using ShortUrl.CodeGenerator.API.Domain;

namespace ShortUrl.CodeGenerator.API.Services;

public interface IAllocationTracker
{
    Task TrackAsync(IEnumerable<ShortCode> codes, string requesterService, DateTimeOffset ts, CancellationToken ct);
}
