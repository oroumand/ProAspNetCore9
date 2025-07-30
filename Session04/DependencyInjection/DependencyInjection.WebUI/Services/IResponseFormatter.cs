namespace DependencyInjection.WebUI.Services;

public interface IResponseFormatter
{
    Task Fromat(HttpContext context, string content);

}
