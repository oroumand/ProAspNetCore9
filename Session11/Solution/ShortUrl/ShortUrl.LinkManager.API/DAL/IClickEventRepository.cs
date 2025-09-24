using ShortUrl.LinkManager.API.Domain;

namespace ShortUrl.LinkManager.API.DAL;

public interface IClickEventRepository
{
    Task AddAsync(ClickEvent ev, CancellationToken ct);
    Task<IReadOnlyList<ClickEvent>> GetRecentAsync(int take, CancellationToken ct);
}
