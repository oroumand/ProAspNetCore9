namespace DependencyInjection.WebUI.Services;

public interface ICounter
{
    int Add(int input);
}

public class DummyCounter : ICounter
{
    public int Add(int input)
    {
        return ++input; 
    }
}
public class TextFormatter : IResponseFormatter
{
    public TextFormatter(ICounter counter)
    {
        _counter = counter;
    }

    private int responseCounter = 0;
    private readonly ICounter _counter;

    public async Task Fromat(HttpContext context, string content)
    {
        responseCounter = _counter.Add(responseCounter);
        await context.Response.WriteAsync($"Count:{responseCounter}\r\n body:{content}");
    }
}


public enum FormmaterType
{
    Text,
    Html
}