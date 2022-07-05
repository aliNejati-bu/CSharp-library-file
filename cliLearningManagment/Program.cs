using System;
using System.Text;
using cliLearningManagment.Entities;
using cliLearningManagment.Repositories;
using cliLearningManagment.Repositories.Exceptions;

class Program
{
    static void Main(string[] args)
    {
        // clear Console
        Console.Write("Login As(1 Admin,2 Teacher,3 Student): ");
        short command = short.Parse(Console.In.ReadLine() ?? "1");

        if (command == 1)
        {
            AdminRun();
        }
        else if (command == 2)
        {
        }
        else
        {
        }
    }

    static void AdminRun()
    {
        // clear Console
        Console.Clear();
        Admin admin = new Admin();

        Console.Write("Enter UserName: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Enter password: ");
        string password = Console.ReadLine() ?? "";

        if (!admin.Login(name, password))
        {
            Console.WriteLine("UserName And password not match.");
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Chooses: ");
            Console.WriteLine("\t 1) create Teacher");
            Console.WriteLine("\t 2) create Course.");
            Console.WriteLine("\t 3) create Student.");
            Console.WriteLine("\t 4) add Course To Student.");
            Console.Write("Please Enter Command: ");
            short command = short.Parse(Console.In.ReadLine() ?? "1");

            if (command == 1)
            {
                CreateTeacher();
            }
            else if (command == 2)
            {
                CreateCourse();
            }
        }
    }


    static void CreateTeacher()
    {
        Console.Clear();
        Console.WriteLine("Creating A Teacher... ");

        Console.Write("Enter Teacher Name: ");
        string teacherName = Console.In.ReadLine() ?? "";

        Console.Write("Enter Teacher Password: ");
        string teacherPassword = Console.In.ReadLine() ?? "";

        try
        {
            Teacher t = TeacherRepository.Instance.AddTeacher(new Teacher(teacherName, teacherPassword));
            Console.WriteLine("Teacher Created.");
            Console.WriteLine($"Teacher Id: {t.Id}");
            Console.ReadKey();
            return;
        }
        catch (DuplicateException)
        {
            Console.Clear();
            Console.WriteLine("Teacher Exists.");
            Console.ReadKey();
            return;
        }
    }


    static void CreateCourse()
    {
        Console.Clear();

        Console.WriteLine("Creating Course... ");

        Console.Write("Enter Name: ");
        string name = Console.In.ReadLine() ?? "";

        Console.Write("Please Enter Teacher Name: ");
        string teacherName = Console.In.ReadLine() ?? "";

        Teacher? teacher = TeacherRepository.Instance.FindByName(teacherName);
        if (teacher == null)
        {
            Console.Clear();
            Console.WriteLine("Teacher Not Exists.");
            Console.ReadKey();
            return;
        }


        Console.Clear();
        Course course = CourseRepository.Instance.AddCourse(new Course(name, teacher.Id ?? ""));
        Console.WriteLine("Course Created...");
        Console.WriteLine($"Course id = {course.Id}");
        Console.ReadKey();
        return;
    }
    
}