namespace ShortUrl.LinkManager.API.Dtos;

public class ReportSummaryResponse
{
    public long TotalLinks { get; set; }
    public long ActiveLinks { get; set; }
    public long DisabledLinks { get; set; }
    public long ExpiredLinks { get; set; }
}