using cliLearningManagment.Entities;
using cliLearningManagment.Repositories.Exceptions;

namespace cliLearningManagment.Repositories;

public class CourseRepository
{
    private static CourseRepository? _instance = null;

    public static CourseRepository Instance => _instance ??= new CourseRepository();


    private CourseRepository()
    {
        if (!File.Exists(@"courses.txt"))
        {
            using (FileStream fileStream = File.Create(@"courses.txt"))
            {
                fileStream.Write(new ReadOnlySpan<byte>());
            }
        }
    }

    public Course? FindById(string id)
    {
        foreach (string line in File.ReadLines(@"courses.txt"))
        {
            string[] lineData = line.Split('|');
            if (lineData[0] == id)
            {
                return new Course(lineData[1], lineData[2], lineData[0]);
            }
        }

        return default;
    }


    public Course AddCourse(Course course)
    {
        Teacher? teacher = TeacherRepository.Instance.FindById(course.TeacherId);
        if (teacher == null)
        {
            throw new BaseDataException("Invalid Teacher.");
        }

        string id = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
        course.Id = id;

        File.AppendAllText(@"courses.txt", $"{course.Id}|{course.Name}|{course.TeacherId}{Environment.NewLine}");

        return course;
    }

    public List<Course> GetAdminCourses(string teacherId)
    {
        List<Course> courses = new List<Course>();
        foreach (string line in File.ReadLines(@"courses.txt"))
        {
            string[] lineData = line.Split('|');
            if (lineData[2] == teacherId)
            {
                courses.Add(FindById(lineData[0]));
            }
        }

        return courses;
    }
}