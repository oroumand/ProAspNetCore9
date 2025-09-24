namespace ShortUrl.RazorShortener.Models;

public class CreateLinkResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string ShortUrl { get; set; } = default!;
}