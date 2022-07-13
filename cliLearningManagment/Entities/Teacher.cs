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


        List<Student> students = StudentCourseRelation.Instance.CourseStudents(courseId);
        flag = false;

        foreach (Student student in students)
        {
            if (student.Id == studentId)
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

    public bool Login()
    {
        Teacher? inFileTeacher = TeacherRepository.Instance.FindByName(Name);
        if (inFileTeacher == null)
        {
            return false;
        }

        if (Password != inFileTeacher.Password)
        {
            return false;
        }

        Id = inFileTeacher.Id;

        return true;
    }

    public List<Course> GetCourses()
    {
        if (Id == null)
        {
            return new List<Course>();
        }

        List<Course> courses = CourseRepository.Instance.GetTeacherCourses(Id);
        return courses;
    }
}