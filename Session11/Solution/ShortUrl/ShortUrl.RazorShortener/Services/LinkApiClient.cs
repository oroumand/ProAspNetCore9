using Microsoft.Extensions.Options;
using ShortUrl.RazorShortener.Models;
using ShortUrl.RazorShortener.Options;
using System.Text.Json;

namespace ShortUrl.RazorShortener.Services;

public class LinkApiClient : ILinkApiClient
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _json = new(JsonSerializerDefaults.Web);

    public LinkApiClient(HttpClient http, IOptions<ApiOptions> opt)
    {
        _http = http;
        var baseUrl = opt.Value.BaseUrl?.TrimEnd('/') + "/";
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new InvalidOperationException("Api:BaseUrl is not configured.");
        _http.BaseAddress = new Uri(baseUrl);
    }

    public async Task<CreateLinkResponse> CreateLinkAsync(CreateLinkRequest req, CancellationToken ct)
    {
        using var resp = await _http.PostAsJsonAsync("api/links", req, _json, ct);
        if (!resp.IsSuccessStatusCode)
        {
            var body = await resp.Content.ReadAsStringAsync(ct);
            throw new HttpRequestException($"CreateLink failed: {(int)resp.StatusCode} {resp.ReasonPhrase}. Body: {body}");
        }
        var dto = await resp.Content.ReadFromJsonAsync<CreateLinkResponse>(_json, ct)
                  ?? throw new InvalidOperationException("Empty response from API.");
        return dto;
    }

    public async Task<ReportSummaryResponse> GetSummaryAsync(CancellationToken ct)
    {
        var dto = await _http.GetFromJsonAsync<ReportSummaryResponse>("api/reports/summary", _json, ct)
                  ?? new ReportSummaryResponse();
        return dto;
    }
}
