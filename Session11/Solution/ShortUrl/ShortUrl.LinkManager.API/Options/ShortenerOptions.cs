namespace ShortUrl.LinkManager.API.Options;

public class ShortenerOptions
{
    public string BaseUrl { get; set; } = "https://localhost:7001";
    public int DefaultCodeLength { get; set; } = 7;
    public int PoolMin { get; set; } = 50;
    public int PoolFill { get; set; } = 500;
}
