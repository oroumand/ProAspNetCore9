namespace MiddlewareSamples;

public interface IMyService
{
    void Add();
}
public class MyService:IMyService
{
    private int _Counter;
    public MyService()
    {
        
    }

    public void Add()
    {
        _Counter++;
    }
}