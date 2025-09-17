using System.Security.Cryptography;
using System.Text;

namespace ShortUrl.CodeGenerator.API.Utils;

public static class CodeFactory
{
    // الفبای Base62: 0-9, a-z, A-Z
    private const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static string NewCode(int length)
    {
        var bytes = RandomNumberGenerator.GetBytes(length);
        var sb = new StringBuilder(length);
        foreach (var b in bytes)
            sb.Append(Alphabet[b % Alphabet.Length]);
        return sb.ToString();
    }
}