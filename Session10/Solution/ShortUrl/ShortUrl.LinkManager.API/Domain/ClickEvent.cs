namespace ShortUrl.LinkManager.API.Domain;

public class ClickEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid LinkId { get; set; }                  // ارجاع به لینک
    public string Code { get; set; } = default!;      // برای کوئری سریع بدون join
    public DateTimeOffset Ts { get; set; } = DateTimeOffset.UtcNow;

    public string? Ip { get; set; }
    public string? UserAgent { get; set; }
    public string? Referrer { get; set; }
}
