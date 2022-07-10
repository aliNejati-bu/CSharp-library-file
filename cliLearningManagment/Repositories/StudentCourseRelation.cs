using System.Text.RegularExpressions;
using cliLearningManagment.Entities;
using cliLearningManagment.Repositories.Exceptions;

namespace cliLearningManagment.Repositories;

public class StudentCourseRelation
{
    private static StudentCourseRelation? _instance = null;

    public static StudentCourseRelation Instance => _instance ??= new StudentCourseRelation();


    private StudentCourseRelation()
    {
        if (!File.Exists(@"courseStudent.txt"))
        {
            using (FileStream fileStream = File.Create(@"courseStudent.txt"))
            {
                fileStream.Write(new ReadOnlySpan<byte>());
            }
        }
    }


    public List<Student> CourseStudents(string courseId)
    {
        List<Student> students = new List<Student>();
        foreach (string line in File.ReadLines(@"courseStudent.txt"))
        {
            string[] lineData = line.Split('|');
            if (lineData[1] == courseId)
            {
                students.Add(StudentRepository.Instance.FindById(lineData[0]));
            }
        }

        return students;
    }


    public List<Course> StudentCourses(string studentId)
    {
        List<Course> students = new List<Course>();
        foreach (string line in File.ReadLines(@"courseStudent.txt"))
        {
            string[] lineData = line.Split('|');
            if (lineData[0] == studentId)
            {
                students.Add(CourseRepository.Instance.FindById(lineData[1]));
            }
        }

        return students;
    }


    public bool CreateRelation(string studentId, string coursesId)
    {
        try
        {
            File.AppendAllText(@"courseStudent.txt", $"{studentId}|{coursesId}|0{Environment.NewLine}");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }


    public bool SetGrade(string studentId, string courseId, string grader)
    {
        File.WriteAllText(@"courseStudent.txt",
            Regex.Replace(File.ReadAllText(@"courseStudent.txt"), @$"({studentId}\|{courseId})\|(\d+)",
                $"$1|{grader}"));
        return true;
    }
}