using cliLearningManagment.Repositories;

namespace cliLearningManagment.Entities;

public class Course
{
    public string? Id;
    public readonly string Name;
    public readonly string TeacherId;

    public Teacher? Teacher => TeacherRepository.Instance.FindById(TeacherId);

    public Course(string name, string teacherId, string? id = null)
    {
        Id = id;
        Name = name;
        TeacherId = teacherId;
    }
}