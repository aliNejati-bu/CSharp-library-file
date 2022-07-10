using cliLearningManagment.Repositories;

namespace cliLearningManagment.Entities;

public class Teacher
{
    public string? Id;
    public string Name;
    public string Password;

    public Teacher(string name, string password, string? id = null)
    {
        Id = id;
        Name = name;
        Password = password;
    }

    public bool SetGrade(string courseId, string studentId, string grade)
    {
        if (Id == null)
        {
            return false;
        }

        List<Course> courses = CourseRepository.Instance.GetTeacherCourses(Id);

        bool flag = false;

        foreach (Course course in courses)
        {
            if (course.Id == courseId)
            {
                flag = true;
                break;
            }
        }

        if (!flag)
        {
            return false;
        }

        return StudentCourseRelation.Instance.SetGrade(studentId, courseId, grade);
    }
}