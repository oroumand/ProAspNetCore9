namespace HttpSamples;

public  class MyController
{
    public  Dictionary<string,Func<HttpContext, Task>> MyRouter = new Dictionary<string,Func<HttpContext, Task>>();
    public const string GetStudentKEY = $"GET_STUDENTS";
    public const string AddStudentKEY = $"POST_STUDENTS";
    public MyController()
    {
        MyRouter[GetStudentKEY] = GetStudent;
        MyRouter[AddStudentKEY] = AddStudent;
    }
    public static async Task GetStudent(HttpContext context)
    {
        await context.Response.WriteAsync($"{context.Request.Method}\r\n");
        await context.Response.WriteAsync($"{context.Request.Path}\r\n");
        var students = StudentRepository.GetStudents();
        foreach (var item in students)
        {
            await context.Response.WriteAsync($"Id:{item.Id} FirstName:{item.FirstName} LastName:{item.LastName} Grade:{item.Grade}\r\n");

        }
    }
    public static async Task AddStudent(HttpContext context)
    {

    }

    public static async Task Delete(HttpContext context)
    {
    }

}
