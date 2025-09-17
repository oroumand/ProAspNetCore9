using Microsoft.Extensions.Options;
using ShortUrl.LinkManager.API.Dtos;
using ShortUrl.LinkManager.API.Options;
using System.Text;
using System.Text.Json;

namespace ShortUrl.LinkManager.API.Clients;

public class CodeGeneratorClient : ICodeGeneratorClient
{
    private readonly HttpClient _http;
    private readonly CodeGenOptions _opt;
    private static readonly JsonSerializerOptions _json = new(JsonSerializerDefaults.Web);

    public CodeGeneratorClient(HttpClient http, IOptions<CodeGenOptions> opt)
    {
        _http = http;
        _opt = opt.Value;

        // اطمینان از BaseAddress
        if (_http.BaseAddress is null)
        {
            if (string.IsNullOrWhiteSpace(_opt.BaseUrl))
                throw new InvalidOperationException("CodeGenerator.BaseUrl is not configured.");

            var baseUrl = _opt.BaseUrl.EndsWith("/") ? _opt.BaseUrl : _opt.BaseUrl + "/";
            _http.BaseAddress = new Uri(baseUrl);
        }
    }

    public async Task<IReadOnlyList<string>> RequestCodesAsync(int count, int length, string requesterService, CancellationToken ct)
    {
        if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (length < 4 || length > 64) throw new ArgumentOutOfRangeException(nameof(length));
        if (string.IsNullOrWhiteSpace(requesterService)) throw new ArgumentException("requesterService is required", nameof(requesterService));

        var payload = new GenerateCodesRequest
        {
            Count = count,
            Length = length,
            RequesterService = requesterService
        };
        
        using var resp = await _http.PostAsJsonAsync("api/code", payload, _json, ct);
        if (!resp.IsSuccessStatusCode)
        {
            var body = await resp.Content.ReadAsStringAsync(ct);
            throw new HttpRequestException($"CodeGenerator returned {(int)resp.StatusCode} {resp.ReasonPhrase}. Body: {body}");
        }

        var dto = await resp.Content.ReadFromJsonAsync<GenerateCodesResponse>(_json, ct)
                  ?? throw new InvalidOperationException("CodeGenerator returned empty response.");

        if (dto.Codes is null || dto.Codes.Count == 0)
            throw new InvalidOperationException("CodeGenerator returned no codes.");

        return dto.Codes;
    }

    // ======= Internal DTOs for (de)serialization =======
    private sealed class GenerateCodesRequest
    {
        public int Count { get; set; }
        public int Length { get; set; }
        public string RequesterService { get; set; } = default!;
    }

    private sealed class GenerateCodesResponse
    {
        public string RequesterService { get; set; } = default!;
        public DateTimeOffset DeliveredAt { get; set; }
        public List<string> Codes { get; set; } = new();
    }
}
