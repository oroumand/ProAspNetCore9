using ShortUrl.RazorShortener.Models;

namespace ShortUrl.RazorShortener.Services;

public interface ILinkApiClient
{
    Task<CreateLinkResponse> CreateLinkAsync(CreateLinkRequest req, CancellationToken ct);
    Task<ReportSummaryResponse> GetSummaryAsync(CancellationToken ct);
}