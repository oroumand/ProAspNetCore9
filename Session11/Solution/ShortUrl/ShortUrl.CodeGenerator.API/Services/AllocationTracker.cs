using ShortUrl.CodeGenerator.API.DAL;
using ShortUrl.CodeGenerator.API.Domain;

namespace ShortUrl.CodeGenerator.API.Services;

public class AllocationTracker : IAllocationTracker
{
    private readonly ICodeRepository _repo;
    public AllocationTracker(ICodeRepository repo) => _repo = repo;

    public async Task TrackAsync(IEnumerable<ShortCode> codes, string requesterService, DateTimeOffset ts, CancellationToken ct)
    {
        await _repo.MarkAllocatedAsync(codes.Select(x => x.Id), requesterService, ts, ct);
    }
}