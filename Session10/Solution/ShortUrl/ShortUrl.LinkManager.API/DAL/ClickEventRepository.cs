using Microsoft.EntityFrameworkCore;
using ShortUrl.LinkManager.API.Domain;

namespace ShortUrl.LinkManager.API.DAL;

public class ClickEventRepository : IClickEventRepository
{
    private readonly LinkDbContext _db;
    public ClickEventRepository(LinkDbContext db) => _db = db;

    public async Task AddAsync(ClickEvent ev, CancellationToken ct)
    {
        _db.ClickEvents.Add(ev);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<ClickEvent>> GetRecentAsync(int take, CancellationToken ct)
    {
        take = Math.Clamp(take, 1, 500);
        return await _db.ClickEvents.AsNoTracking()
            .OrderByDescending(x => x.Ts)
            .Take(take)
            .ToListAsync(ct);
    }
}