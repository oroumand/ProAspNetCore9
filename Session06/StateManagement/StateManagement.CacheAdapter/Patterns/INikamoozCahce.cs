namespace StateManagement.CacheAdapter.Patterns;

public interface INikamoozCahce
{
    public T Get<T>(string key);
    public void Set<T>(string key, T value);
}

public class NikamoozMemoryCahce : INikamoozCahce
{
    public T Get<T>(string key)
    {
        throw new NotImplementedException();
    }

    public void Set<T>(string key, T value)
    {
        throw new NotImplementedException();
    }
}

public class NikamoozDistributedCache : INikamoozCahce
{
    public T Get<T>(string key)
    {
        throw new NotImplementedException();
    }

    public void Set<T>(string key, T value)
    {
        throw new NotImplementedException();
    }
}

public static class CacheExtensions
{
    public static IServiceCollection AddNikamoozDis(this IServiceCollection services)
    {
        services.AddDistributedSqlServerCache(c =>
        {

        });
        services.AddScoped<INikamoozCahce, NikamoozDistributedCache>();
        return services;
    }

    public static IServiceCollection AddNikamoozMemory(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped<INikamoozCahce, NikamoozMemoryCahce>();
        return services;
    }
}