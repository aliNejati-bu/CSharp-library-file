using System.Collections;
using System.Text;

namespace cliLearningManagment.Entities;

using System.IO;

public class Admin
{
    public Admin()
    {
        // check if file exists.
        if (!File.Exists("Admins.txt"))
        {
            FileStream fileStream = File.Create(@"Admins.txt");
            fileStream.Write(new UTF8Encoding(true).GetBytes("ali|13811381my"));
            fileStream.Close();
        }
    }

    private string password = "";
    private string name = "";

    /// <summary>
    /// get user name and password than doLogin
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="userPassword"></param>
    /// <returns>
    /// false: username and password not match.
    /// true: login in valid.
    /// </returns>
    public bool Login(string userName, string userPassword)
    {
        this.name = userName;
        this.password = userPassword;

        Console.Clear();

        IEnumerable<string> lines = File.ReadLines(@"Admins.txt");
        foreach (string line in lines)
        {
            var admin = line.Split('|');
            if (admin[0] == name)
            {
                if (admin[1] == password)
                {
                    return true;
                }

                return false;
            }
        }

        return false;
    }
}