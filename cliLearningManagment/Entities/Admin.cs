using System.Collections;

namespace cliLearningManagment.Entities;

using System.IO;

public class Admin
{
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