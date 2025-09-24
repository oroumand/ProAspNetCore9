using ShortUrl.CodeGenerator.API.Domain;

namespace ShortUrl.CodeGenerator.API.DAL;

public interface ICodeRepository
{
    Task AddCodesAsync(IEnumerable<ShortCode> codes, CancellationToken ct);
    Task<long> CountByStatusAsync(int length, ShortCodeStatus status, CancellationToken ct);
    Task MarkAllocatedAsync(IEnumerable<Guid> codeIds, string requester, DateTimeOffset ts, CancellationToken ct);
}