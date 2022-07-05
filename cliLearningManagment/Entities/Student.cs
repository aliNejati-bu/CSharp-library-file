namespace cliLearningManagment.Entities;

public class Student
{
    public string? Id;
    public string Name;
    public string Password;

    public Student(string name, string password, string? id = null)
    {
        Id = id;
        Name = name;
        Password = password;
    }
}