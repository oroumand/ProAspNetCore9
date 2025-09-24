using ShortUrl.CodeGenerator.API.DAL;
using ShortUrl.CodeGenerator.API.Domain;
using ShortUrl.CodeGenerator.API.Dtos;
using ShortUrl.CodeGenerator.API.Utils;

namespace ShortUrl.CodeGenerator.API.Services;

public class CodeGenerationService : ICodeGenerationService
{
    private readonly ICodeRepository _repo;
    private readonly IAllocationTracker _tracker;

    public CodeGenerationService(ICodeRepository repo, IAllocationTracker tracker)
    {
        _repo = repo;
        _tracker = tracker;
    }

    public async Task<GenerateCodesResponse> GenerateBatchAsync(GenerateCodesRequest req, CancellationToken ct)
    {
        if (req.Count <= 0) throw new ArgumentOutOfRangeException(nameof(req.Count));
        if (req.Length < 4 || req.Length > 64) throw new ArgumentOutOfRangeException(nameof(req.Length));

        // تولید کدهای یکتا (در صورت برخورد، constraint دیتابیس جلوی تکرار را می‌گیرد)
        var codes = new List<ShortCode>(req.Count);
        for (int i = 0; i < req.Count; i++)
        {
            codes.Add(new ShortCode
            {
                Value = CodeFactory.NewCode(req.Length),
                Length = req.Length,
                Status = ShortCodeStatus.Available
            });
        }

        await _repo.AddCodesAsync(codes, ct);

        // علامت‌گذاری تخصیص و ساخت پاسخ
        var ts = DateTimeOffset.UtcNow;
        await _tracker.TrackAsync(codes, req.RequesterService, ts, ct);

        return new GenerateCodesResponse
        {
            RequesterService = req.RequesterService,
            DeliveredAt = ts,
            Codes = codes.Select(c => c.Value).ToList()
        };
    }

    public async Task<InventoryStatusResponse> GetInventoryAsync(int length, CancellationToken ct)
    {
        var available = await _repo.CountByStatusAsync(length, ShortCodeStatus.Available, ct);
        var alloc = await _repo.CountByStatusAsync(length, ShortCodeStatus.Allocated, ct);
        var consumed = await _repo.CountByStatusAsync(length, ShortCodeStatus.Consumed, ct);

        return new InventoryStatusResponse
        {
            Length = length,
            AvailableCount = available,
            AllocatedCount = alloc,
            ConsumedCount = consumed
        };
    }
}