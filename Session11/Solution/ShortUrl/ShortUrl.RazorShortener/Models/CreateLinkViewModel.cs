using System.ComponentModel.DataAnnotations;

namespace ShortUrl.RazorShortener.Models;

public class CreateLinkViewModel
{
    [Required, Url, MaxLength(2048)]
    [Display(Name = "آدرس بلند (URL)")]
    public string LongUrl { get; set; } = default!;

    [Display(Name = "تاریخ انقضا (اختیاری)")]
    public DateTimeOffset? ExpiryDate { get; set; }

    [Range(1, int.MaxValue)]
    [Display(Name = "حداکثر کلیک (اختیاری)")]
    public int? MaxClicks { get; set; }

    [Range(4, 64)]
    [Display(Name = "طول کد (اختیاری)")]
    public int? CodeLength { get; set; }

    // خروجی
    public string? ShortUrl { get; set; }
}