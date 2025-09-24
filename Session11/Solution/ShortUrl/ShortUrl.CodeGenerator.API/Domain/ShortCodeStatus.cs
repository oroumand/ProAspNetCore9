namespace ShortUrl.CodeGenerator.API.Domain;

public enum ShortCodeStatus
{
    Available = 0,
    Allocated = 1,
    Consumed = 2,
    Disabled = 3
}
