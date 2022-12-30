using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
[Serializable]
class Account
{
    private string username { get; set; }
    private string password { get; set; }
    private string savedUsername { get; set; }
    private string savedPassword { get; set; }

    public void signUp()
    {
        try
        {
            Console.WriteLine();
            Console.Write("Please enter a username: ");
            this.username = Console.ReadLine().ToLower();
            Console.Write("Please enter a password: ");
            this.password = Console.ReadLine().ToLower();
            Console.WriteLine("SIGNUP COMPLETE");
            Console.WriteLine("LOG IN.....");
            save();
            logIn();
        }

        catch (IOException)
        {
            Console.WriteLine("Error: File acccessed incorrectly");
        }
    }

    public void logIn()
    {
        bool loggedIn = false;
        while (!loggedIn)
        {
            try
            {
                Console.WriteLine();
                Console.Write("Please enter your username: ");
                string username = Console.ReadLine().ToLower();
                Console.Write("Please enter your password: ");
                string password = Console.ReadLine().ToLower();

                load();
                if (username == savedUsername && password == savedPassword)
                {
                    loggedIn = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid username or password, Please try again.");
                }
            }

            catch (IOException)
            {
                Console.WriteLine("Error: File acccessed incorrectly");
            }
        }
    }

    void save()
    {
        try
        {
            FileStream fs = new FileStream("Accounts.txt", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, username);
            bf.Serialize(fs, password);
            fs.Close();
        }

        catch (IOException)
        {
            Console.Write("Error: Exiting......");
            Environment.Exit(0);
        }

        catch (ArgumentException)
        {
            Console.Write("Error: Invalid input");
            Environment.Exit(0);
        }

        catch (SerializationException)
        {
            Console.WriteLine("Error: File corrupt or serialized incorrectly.");
            Environment.Exit(0);
        }

        catch (UnauthorizedAccessException)
        {
            Console.Write("Error: Insufficient privileges");
            Environment.Exit(0);
        }
    }

    void load()
    {
        try
        {
            FileStream fs = new FileStream("Accounts.txt", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            savedUsername = (string)bf.Deserialize(fs);
            savedPassword = (string)bf.Deserialize(fs);
            fs.Close();
        }

        catch (UnauthorizedAccessException)
        {
            Console.Write("Error: Insufficient privileges");
            Environment.Exit(0);
        }

        catch (FileNotFoundException)
        {
            Console.Write("Error: File missing, please create an account first.");
            Environment.Exit(0);
        }

        catch (SerializationException)
        {
            Console.WriteLine("Error: File corrupt or serialized incorrectly.");
            Environment.Exit(0);
        }

        catch (ArgumentException)
        {
            Console.Write("Error: Invalid input");
            Environment.Exit(0);
        }
    }
}




