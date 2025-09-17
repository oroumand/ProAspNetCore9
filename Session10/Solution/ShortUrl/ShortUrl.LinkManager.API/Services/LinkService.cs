using Microsoft.Extensions.Options;
using ShortUrl.LinkManager.API.Clients;
using ShortUrl.LinkManager.API.DAL;
using ShortUrl.LinkManager.API.Domain;
using ShortUrl.LinkManager.API.Dtos;
using ShortUrl.LinkManager.API.Options;

namespace ShortUrl.LinkManager.API.Services;

public class LinkService : ILinkService
{
    private readonly ILinkRepository _repo;
    private readonly ICacheManager _cache;
    private readonly ICodeGeneratorClient _codeGen;
    private readonly ShortenerOptions _opt;

    public LinkService(
        ILinkRepository repo,
        ICacheManager cache,
        ICodeGeneratorClient codeGen,
        IOptions<ShortenerOptions> opt)
    {
        _repo = repo;
        _cache = cache;
        _codeGen = codeGen;
        _opt = opt.Value;
    }

    public async Task<CreateLinkResponse> CreateAsync(CreateLinkRequest req, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(req.LongUrl))
            throw new ArgumentException("LongUrl is required");

        var length = req.CodeLength ?? _opt.DefaultCodeLength;

        // 1) تلاش برای گرفتن کد از استخر کش
        var code = await _cache.PopCodeAsync(length, ct);

        // 2) اگر موجود نبود، از CodeGenerator درخواست و استخر را پر کن
        if (string.IsNullOrEmpty(code))
        {
            var fresh = await _codeGen.RequestCodesAsync(_opt.PoolFill, length, "link-api", ct);
            await _cache.PushCodesAsync(length, fresh, ct);
            code = await _cache.PopCodeAsync(length, ct)
                   ?? throw new InvalidOperationException("Failed to acquire code from pool after refill");
        }
        else
        {
            // اگر نزدیک حداقل بود، به‌صورت هم‌زمان استخر را پر کن (fire-and-forget ایمن)
            _ = TopUpIfLowAsync(length, ct);
        }

        // 3) ذخیره نگاشت در DB
        var link = new Link
        {
            Code = code!,
            LongUrl = req.LongUrl,
            Status = LinkStatus.Active,
            ExpiryDate = req.ExpiryDate,
            MaxClicks = req.MaxClicks
        };
        link = await _repo.AddAsync(link, ct);

        // 4) پاسخ
        var shortUrl = $"{_opt.BaseUrl.TrimEnd('/')}/{link.Code}";
        return new CreateLinkResponse { Id = link.Id, Code = link.Code, ShortUrl = shortUrl };
    }

    private async Task TopUpIfLowAsync(int length, CancellationToken ct)
    {
        try
        {
            var size = await _cache.PoolSizeAsync(length, ct);
            if (size < _opt.PoolMin)
            {
                var fresh = await _codeGen.RequestCodesAsync(_opt.PoolFill, length, "link-api", ct);
                await _cache.PushCodesAsync(length, fresh, ct);
            }
        }
        catch
        {
            // لاگ در نسخه‌های بعدی: فعلاً بی‌صدا
        }
    }
}