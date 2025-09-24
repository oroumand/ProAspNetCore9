namespace ShortUrl.LinkManager.API.Dtos;

public class LinkDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string LongUrl { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
}