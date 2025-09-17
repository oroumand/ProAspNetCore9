namespace ShortUrl.CodeGenerator.API.Domain;



public class ShortCode
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Value { get; set; } = default!;      // کد کوتاه (Base62)
    public int Length { get; set; }                    // طول کد
    public ShortCodeStatus Status { get; set; } = ShortCodeStatus.Available;
    public string? AllocatedToService { get; set; }    // شناسه سرویس درخواست‌دهنده
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? AllocatedAt { get; set; }
    public DateTimeOffset? DeliveredAt { get; set; }   // زمان تحویل به Link API
}