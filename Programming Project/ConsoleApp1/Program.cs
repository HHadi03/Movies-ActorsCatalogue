using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            while (true)
            {
                Account account = new Account();
                Console.WriteLine("=======================================");
                Console.WriteLine("Welcome to the Actors & Movie Catalogue!");
                Console.WriteLine();
                Console.WriteLine("1. Sign Up");
                Console.WriteLine("2. Log In");

                string selection = args.Length > 0 ? args[0] : "";
                if (selection == "")
                {
                    Console.WriteLine();
                    Console.Write("Input: ");
                    selection = Console.ReadLine();
                }

                switch (selection)
                {
                    case "1":
                        account.signUp();
                        break;

                    case "2":
                        account.logIn();
                        break;

                    default:
                        continue;
                }

                break;
            }

            Catalogue catalogue = new Catalogue();
            catalogue.homePage();
        }

        catch (Exception ex)
        {
            List<string> list = new() { ex.Message, ex.StackTrace };
            File.AppendAllLines("log.txt", list);
            Console.WriteLine("An unexpected error has occured");
        }
    }
}










