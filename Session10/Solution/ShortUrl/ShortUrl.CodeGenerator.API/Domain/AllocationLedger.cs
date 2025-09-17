namespace ShortUrl.CodeGenerator.API.Domain;

public class AllocationLedger
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string RequesterService { get; set; } = default!;
    public int Count { get; set; }
    public int Length { get; set; }
    public DateTimeOffset DeliveredAt { get; set; } = DateTimeOffset.UtcNow;
}
