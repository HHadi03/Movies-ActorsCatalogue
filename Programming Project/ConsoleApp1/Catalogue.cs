using System.Globalization;
[Serializable]
class Catalogue
{
    protected string name { get; set; }
    protected string year { get; set; }
    protected string description { get; set; }

    public Catalogue (string name, string year, string description)
    {
        this.name = name;
        this.year = year;
        this.description = description;
    }
    
    public Catalogue()
    {

    }

    public void homePage()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("***************************");
            Console.WriteLine("* " + "Catalogue" + " *");
            Console.WriteLine("***************************");
            Console.WriteLine();
            Console.WriteLine("1. Movies");
            Console.WriteLine("2. Actors");
            Console.WriteLine("3. Quit");
            Console.WriteLine();
            try
            {
                Console.Write("Selection: ");
                string selection = Console.ReadLine();

                if (inputValidation(selection))
                {
                    if (selection == "1")
                    {
                        Movie movie = new Movie();
                        movie.moviesPage();
                    }

                    else if (selection == "2")
                    {
                        Actor actor = new Actor();
                        actor.actorsPage();
                    }

                    else if (selection == "3")
                    {
                        Console.Write("Session Ended, Goodbye!");
                        break;
                    }

                    break;
                }

                else
                {
                    Console.WriteLine("Please enter a valid selection");
                }
            }

            catch (ArgumentException)
            {
                Console.WriteLine("Error: Invalid range, please use correct input");
            }
        }
    }

    public bool inputValidation(string selection)
    {
        if (selection == "1" || selection == "2" || selection == "3")
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    
    //https://learn.microsoft.com/en-us/dotnet/api/system.datetime.parseexact?view=net-7.0
    public void dateCheck(string date)
    {
        bool valid = false;
        while (!valid)
        {
            try
            {
                DateTime.ParseExact(date, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                valid = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Date is invalid. Please try again.");
                Console.Write("Enter a new date in the format (DD/MM/YYYY): ");
                date = Console.ReadLine().ToLower();
            }
        }
    }
}

 
