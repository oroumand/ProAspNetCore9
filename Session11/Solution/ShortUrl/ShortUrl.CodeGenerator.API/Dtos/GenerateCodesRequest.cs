namespace ShortUrl.CodeGenerator.API.Dtos;

public class GenerateCodesRequest
{
    public int Count { get; set; }          // چندتا کد؟
    public int Length { get; set; } = 7;    // طول کد
    public string RequesterService { get; set; } = "link-api"; // شناسه سرویس درخواست‌دهنده
}
