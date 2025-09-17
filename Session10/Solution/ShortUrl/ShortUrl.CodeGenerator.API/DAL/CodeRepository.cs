using Microsoft.EntityFrameworkCore;
using ShortUrl.CodeGenerator.API.Domain;

namespace ShortUrl.CodeGenerator.API.DAL;

public class CodeRepository : ICodeRepository
{
    private readonly CodeDbContext _db;
    public CodeRepository(CodeDbContext db) => _db = db;

    public async Task AddCodesAsync(IEnumerable<ShortCode> codes, CancellationToken ct)
    {
        await _db.ShortCodes.AddRangeAsync(codes, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<long> CountByStatusAsync(int length, ShortCodeStatus status, CancellationToken ct)
    {
        return await _db.ShortCodes.AsNoTracking()
            .Where(x => x.Length == length && x.Status == status)
            .LongCountAsync(ct);
    }

    public async Task MarkAllocatedAsync(IEnumerable<Guid> codeIds, string requester, DateTimeOffset ts, CancellationToken ct)
    {
        var codes = await _db.ShortCodes.Where(x => codeIds.Contains(x.Id)).ToListAsync(ct);
        foreach (var c in codes)
        {
            c.Status = ShortCodeStatus.Allocated;
            c.AllocatedToService = requester;
            c.AllocatedAt = ts;
            c.DeliveredAt = ts;
        }
        await _db.SaveChangesAsync(ct);

        _db.AllocationLedgers.Add(new AllocationLedger
        {
            RequesterService = requester,
            Count = codes.Count,
            Length = codes.FirstOrDefault()?.Length ?? 0,
            DeliveredAt = ts
        });
        await _db.SaveChangesAsync(ct);
    }
}