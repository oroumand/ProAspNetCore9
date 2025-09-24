namespace ShortUrl.RazorShortener.Models;

public class TopReferrerDto
{
    public string Referrer { get; set; } = default!;
    public int Count { get; set; }
}