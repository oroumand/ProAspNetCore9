

namespace HttpSamples;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }
    public int Grade { get; set; }
}

public class StudentRepository
{
    private static List<Student> _students = [
        new Student{
            Id = 1,
            FirstName="Mherzad",
            LastName = "Hassani",
            Grade = 20
        },
                new Student{
            Id = 2,
            FirstName="Alireza",
            LastName = "Oroumand",
            Grade = 100
        }
        ];
    public static List<Student> GetStudents() => _students;

    internal static void CreateStudent(Student student)
    {
        _students.Add(student);
    }

    internal static void Delete(int id)
    {
        var student = _students.FirstOrDefault(c=>c.Id == id);
        _students.Remove(student);
    }
}