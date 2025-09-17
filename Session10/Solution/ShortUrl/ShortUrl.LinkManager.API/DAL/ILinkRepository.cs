using ShortUrl.LinkManager.API.Domain;

namespace ShortUrl.LinkManager.API.DAL;

public interface ILinkRepository
{
    Task<Link> AddAsync(Link link, CancellationToken ct);
    Task<Link?> FindByCodeAsync(string code, CancellationToken ct);
    Task<(long total, long active, long disabled, long expired)> SummaryAsync(CancellationToken ct);
}
