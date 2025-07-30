namespace DependencyInjection.WebUI.Services;

public class HtmlFormatter : IResponseFormatter
{
    public HtmlFormatter()
    {

    }

    private int responseCounter = 0;
    public async Task Fromat(HttpContext context, string content)
    {
        responseCounter++;
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync($"<bold>Count:</bold>{responseCounter}<br/> <bold>body:</bold>{content}");
    }
}