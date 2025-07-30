namespace DependencyInjection.WebUI.Services;

public static class TextFormatterExtensions
{
    public static IServiceCollection AddTextFormatter(this IServiceCollection services)
    {
        //services.AddTransient<IResponseFormatter>(sp =>
        //{

        //    DateTime dateTime = DateTime.Now;
        //    if (dateTime.Hour > 8 && dateTime.Hour < 12)
        //    {
        //        var counter = sp.GetRequiredService<ICounter>();
        //        return new TextFormatter(counter);
        //    }
        //    else
        //    {
        //        return new HtmlFormatter();
        //    }
        //});
        services.AddTransient<ICounter, DummyCounter>();
        services.AddTransient<IResponseFormatter, TextFormatter>();
        services.AddTransient<IResponseFormatter, HtmlFormatter>();
        return services;
    }
}
