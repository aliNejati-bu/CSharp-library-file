using cliLearningManagment.Entities;
using cliLearningManagment.Repositories.Exceptions;

namespace cliLearningManagment.Repositories;

public class StudentRepository
{
    private static StudentRepository? _instance = null;

    public static StudentRepository Instance => _instance ??= new StudentRepository();

    private StudentRepository()
    {
        if(!File.Exists(@"students.txt")){
            using (FileStream fileStream = File.Create(@"students.txt"))
            {
                fileStream.Write(new ReadOnlySpan<byte>());
            }
        }
    }

    public Student? FindByName(string name)
    {
        foreach (string line in File.ReadLines(@"students.txt"))
        {
            string[] lineData = line.Split('|');
            if (lineData[1] == name)
            {
                return new Student(name, lineData[2], lineData[0]);
            }
        }

        return null;
    }

    public Student? FindById(string id)
    {
        foreach (string line in File.ReadLines(@"students.txt"))
        {
            string[] lineData = line.Split('|');
            if (lineData[0] == id)
            {
                return new Student(lineData[1], lineData[2], lineData[0]);
            }
        }

        return default;
    }

    public Student AddStudent(Student student)
    {
        Student? tempStudent = FindByName(student.Name);
        if (tempStudent != null)
        {
            throw new DuplicateException("Student", "name");
        }

        string id = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
        student.Id = id;

        File.AppendAllText(@"students.txt", $"{student.Id}|{student.Name}|{student.Password}{Environment.NewLine}");
        return student;
    }
}