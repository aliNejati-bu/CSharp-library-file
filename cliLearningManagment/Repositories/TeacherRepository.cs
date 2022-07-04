using cliLearningManagment.Entities;
using cliLearningManagment.Repositories.Exceptions;

namespace cliLearningManagment.Repositories;

public class TeacherRepository
{
    private static TeacherRepository? _instance = null;

    public static TeacherRepository Instance => _instance ??= new TeacherRepository();

    private TeacherRepository()
    {
        using (FileStream fileStream = File.Create(@"teachers.txt"))
        {
            fileStream.Write(new ReadOnlySpan<byte>());
        }
    }

    public Teacher? FindByName(string name)
    {
        foreach (string line in File.ReadLines(@"teachers.txt"))
        {
            string[] lineData = line.Split('|');
            if (lineData[1] == name)
            {
                return new Teacher(name, lineData[2], lineData[0]);
            }
        }

        return null;
    }

    public Teacher? FindById(string id)
    {
        foreach (string line in File.ReadLines(@"teachers.txt"))
        {
            string[] lineData = line.Split('|');
            if (lineData[0] == id)
            {
                return new Teacher(lineData[1], lineData[2], lineData[0]);
            }
        }

        return default;
    }

    public Teacher AddTeacher(Teacher teacher)
    {
        Teacher? tempTeacher = FindByName(teacher.Name);
        if (tempTeacher != null)
        {
            throw new DuplicateException("Teacher", "name");
        }

        string id = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
        teacher.Id = id;

        File.AppendAllLines(@"teachers.txt", new[]
        {
            $"{teacher.Id}|{teacher.Name}|{teacher.Password}"
        });
        return teacher;
    }
}