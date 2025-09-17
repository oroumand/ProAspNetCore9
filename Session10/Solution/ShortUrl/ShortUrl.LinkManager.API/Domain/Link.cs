namespace ShortUrl.LinkManager.API.Domain;

public class Link
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = default!;
    public string LongUrl { get; set; } = default!;
    public LinkStatus Status { get; set; } = LinkStatus.Active;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ExpiryDate { get; set; }
    public int? MaxClicks { get; set; } // برای آینده
    public string? Owner { get; set; }  // برای گزارش/ACL در آینده
}
