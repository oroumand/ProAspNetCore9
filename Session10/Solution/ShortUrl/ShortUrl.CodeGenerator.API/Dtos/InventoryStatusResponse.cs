namespace ShortUrl.CodeGenerator.API.Dtos;

public class InventoryStatusResponse
{
    public int Length { get; set; }
    public long AvailableCount { get; set; }
    public long AllocatedCount { get; set; }
    public long ConsumedCount { get; set; }
}