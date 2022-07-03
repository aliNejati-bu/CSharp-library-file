using System;
using System.Text;
using cliLearningManagment.Entities;

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
        
        // check if file exists.
        if (!File.Exists("Admins.txt"))
        {
            FileStream fileStream = File.Create(@"Admins.txt");
            fileStream.Write(new UTF8Encoding(true).GetBytes("ali|13811381my"));
            fileStream.Close();
        }

        Console.Write("Enter UserName: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Enter password: ");
        string password = Console.ReadLine() ?? "";

        if (!admin.Login(name, password))
        {
            Console.WriteLine("UserName And password not match.");
            return;
        }
    }
}