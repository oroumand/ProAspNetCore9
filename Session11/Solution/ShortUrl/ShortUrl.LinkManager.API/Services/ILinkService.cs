using ShortUrl.LinkManager.API.Dtos;

namespace ShortUrl.LinkManager.API.Services;

public interface ILinkService
{
    Task<CreateLinkResponse> CreateAsync(CreateLinkRequest req, CancellationToken ct);
}