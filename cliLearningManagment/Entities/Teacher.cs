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
}