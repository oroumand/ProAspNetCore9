using HttpSamples;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    //string key = $"{context.Request.Method}_{context.Request.Path.ToString().ToUpper()}";
    //await new MyController().MyRouter[key].Invoke(context);
    if (context.Request.Method == "GET")
    {
        if (context.Request.Path.StartsWithSegments("/"))
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync($"{context.Request.Method}\r\n");
            await context.Response.WriteAsync($"{context.Request.Path}\r\n");
            foreach (var item in context.Request.Headers)
            {
                await context.Response.WriteAsync($"Key:{item.Key} Value:{item.Value}\r\n");
            }
        }
        else if (context.Request.Path.StartsWithSegments("/students"))
        {
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync($"{context.Request.Method}<br/>");
            await context.Response.WriteAsync($"{context.Request.Path}<br/>");
            var students = StudentRepository.GetStudents();
            foreach (var item in students)
            {
                await context.Response.WriteAsync($"Id:{item.Id} FirstName:{item.FirstName} LastName:{item.LastName} Grade:{item.Grade}<br/>");

            }
            context.Response.StatusCode = 200;
        }
    }
    else if (context.Request.Method == "POST")
    {
        if (context.Request.Path.StartsWithSegments("/students"))
        {
            StreamReader streamReader = new StreamReader(context.Request.Body);
            string body = await streamReader.ReadToEndAsync();
            Student student = JsonSerializer.Deserialize<Student>(body);
            if (student != null)
            {
                StudentRepository.CreateStudent(student);
                context.Response.StatusCode = 201;
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }
    }
    else if (context.Request.Method == "DELETE")
    {
        if (context.Request.Path.StartsWithSegments("/students"))
        {
            if (context.Request.Query.Keys.Contains("Id"))
            {
                int id = int.Parse(context.Request.Query["Id"]);
                StudentRepository.Delete(id);
                context.Response.StatusCode = 204;
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }

});
app.Run();
