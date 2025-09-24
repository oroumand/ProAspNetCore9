namespace ShortUrl.LinkManager.API.Dtos;

public class RecentClickDto
{
    public DateTimeOffset Ts { get; set; }
    public string Code { get; set; } = default!;
    public string? Ip { get; set; }
    public string? Referrer { get; set; }
    public string? UserAgent { get; set; }
}