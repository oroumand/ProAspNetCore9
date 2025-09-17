namespace ShortUrl.CodeGenerator.API.Dtos;

public class GenerateCodesResponse
{
    public string RequesterService { get; set; } = default!;
    public DateTimeOffset DeliveredAt { get; set; }
    public IReadOnlyList<string> Codes { get; set; } = Array.Empty<string>();
}
