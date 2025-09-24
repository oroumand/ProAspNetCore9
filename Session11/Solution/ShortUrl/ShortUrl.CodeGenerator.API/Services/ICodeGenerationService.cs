using ShortUrl.CodeGenerator.API.Dtos;

namespace ShortUrl.CodeGenerator.API.Services;

public interface ICodeGenerationService
{
    Task<GenerateCodesResponse> GenerateBatchAsync(GenerateCodesRequest req, CancellationToken ct);
    Task<InventoryStatusResponse> GetInventoryAsync(int length, CancellationToken ct);
}
