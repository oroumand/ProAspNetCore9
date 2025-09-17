using Microsoft.EntityFrameworkCore;
using ShortUrl.LinkManager.API.Domain;

namespace ShortUrl.LinkManager.API.DAL;

public class LinkRepository : ILinkRepository
{
    private readonly LinkDbContext _db;
    public LinkRepository(LinkDbContext db) => _db = db;

    public async Task<Link> AddAsync(Link link, CancellationToken ct)
    {
        _db.Links.Add(link);
        await _db.SaveChangesAsync(ct);
        return link;
    }

    public async Task<Link?> FindByCodeAsync(string code, CancellationToken ct)
    {
        return await _db.Links.AsNoTracking().FirstOrDefaultAsync(x => x.Code == code, ct);
    }

    public async Task<(long total, long active, long disabled, long expired)> SummaryAsync(CancellationToken ct)
    {
        var q = _db.Links.AsNoTracking();
        var total = await q.LongCountAsync(ct);
        var active = await q.LongCountAsync(x => x.Status == LinkStatus.Active, ct);
        var disabled = await q.LongCountAsync(x => x.Status == LinkStatus.Disabled, ct);
        var expired = await q.LongCountAsync(x => x.Status == LinkStatus.Expired, ct);
        return (total, active, disabled, expired);
    }
}