namespace ShortUrl.LinkManager.API.Clients;

public interface ICodeGeneratorClient
{
    Task<IReadOnlyList<string>> RequestCodesAsync(int count, int length, string requesterService, CancellationToken ct);
}
