namespace ShortUrl.LinkManager.API.Dtos;

public class CreateLinkRequest
{
    public string LongUrl { get; set; } = default!;
    public DateTimeOffset? ExpiryDate { get; set; }
    public int? MaxClicks { get; set; }
    public int? CodeLength { get; set; } // اگر null باشد از DefaultCodeLength استفاده می‌شود
}
