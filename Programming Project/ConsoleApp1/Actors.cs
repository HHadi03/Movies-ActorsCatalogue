using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
[Serializable]
class Actor : Catalogue
{
    private string gender { get; set; }
    private string popularMovie { get; set; }
    
    public Actor(string name, string year, string description, string gender, string popularMovie) : base(name, year, description)
    {
        this.gender = gender;
        this.popularMovie = popularMovie;
    }

    public Actor()
    {

    }

    private Dictionary<Actor, string> actors = new Dictionary<Actor, string>();

    public void actorsPage()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("***************************");
            Console.WriteLine("* " + "Actors" + " *");
            Console.WriteLine("***************************");
            Console.WriteLine();
            Console.WriteLine("1. Browse Actors");
            Console.WriteLine("2. Add Actors");
            Console.WriteLine("3. Main Menu");
            Console.WriteLine();
            try
            {
                Console.Write("Selection: ");
                string selection = Console.ReadLine();

                if (inputValidation(selection))
                {
                    if (selection == "1")
                    {
                        browseActor();
                    }
                    else if (selection == "2")
                    {
                        addActor();
                    }

                    else if (selection == "3")
                    {
                        homePage();
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

    void browseActor()
    {
        load();
        displayActors();

        while (true)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("B To Go Back");
                Console.Write("SEARCH (BLANK = View All): ");
                string search = Console.ReadLine().ToLower();
                if (search == "b") { actorsPage(); return; }

                bool actorFound = false;
                foreach (KeyValuePair<Actor, string> actor in actors)
                {
                    if (actor.Key.name.Contains(search) || actor.Value.Contains(search))
                    {
                        actorFound = true;
                        Console.WriteLine("");
                        Console.WriteLine("Actors Name:      " + actor.Value);
                        Console.WriteLine("Date Of Birth:    " + actor.Key.year);
                        Console.WriteLine("Gender:           " + actor.Key.gender);
                        Console.WriteLine("Popular Movies:   " + actor.Key.popularMovie);
                        Console.WriteLine("Biography:        " + actor.Key.description);
                    }
                }

                if (!actorFound)
                {
                    Console.WriteLine("");
                    Console.Write("Actor not found....\n" +
                    "Would you like to add this Actor? (Y/N):  ");
                    string input = Console.ReadLine().ToLower();
                    if (input == "y") { addActor(); return; }
                    else if (input == "n") { browseActor(); return; }
                    else { Console.WriteLine("Invalid format, Y or N only"); }
                }
            }

            catch (IOException)
            {
                Console.WriteLine("Error: File read/written incorrectly");
            }
        }
    }

    void addActor()
    {
        save(); load();
        try
        {
            Console.WriteLine("");
            Console.Write("What is the Actors name: ");
            string actorName = Console.ReadLine().ToLower();

            Console.Write("What is the Actors Date Of Birth in the format (DD/MM/YYYY): ");
            string date = Console.ReadLine().ToLower(); dateCheck(date); ;

            Console.Write("What gender are they: ");
            string gender = Console.ReadLine().ToLower();

            Console.Write("What is their most popular movies (followed by a coma): ");
            string popularMovie = Console.ReadLine().ToLower();

            Console.Write("Describe them in a few sentences: ");
            string bio = Console.ReadLine().ToLower();

            Actor newActor = new Actor(name = actorName, year = date, description = bio, gender, popularMovie);
            actors.Add(newActor, actorName);
        }

        catch (IOException)
        {
            Console.WriteLine("Error: please try again later");
        }

        save(); Console.WriteLine("Actor Added Sucesfully");
        actorsPage();
    }

    void displayActors()
    {
        foreach (KeyValuePair<Actor, string> actor in actors)
        {
            Console.WriteLine("");
            Console.WriteLine("Actors Name:      " + actor.Value);
            Console.WriteLine("Date Of Birth:    " + actor.Key.year);
            Console.WriteLine("Gender:           " + actor.Key.gender);
            Console.WriteLine("Popular Movies:   " + actor.Key.popularMovie);
            Console.WriteLine("Biography:        " + actor.Key.description);
        }
    }

    void save()
    {
        try
        {
            FileStream fs = new FileStream("actors.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, actors);
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
            FileStream fs = new FileStream("actors.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            actors = bf.Deserialize(fs) as Dictionary<Actor, string>;
            fs.Close();
        }

        catch (UnauthorizedAccessException)
        {
            Console.Write("Error: Insufficient privileges");
            Environment.Exit(0);
        }

        catch (FileNotFoundException)
        {
            Console.Write("Error: File missing, add Actor Objects first.");
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
