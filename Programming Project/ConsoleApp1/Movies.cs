using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
[Serializable]
class Movie : Catalogue
{
    private string runTime { get; set; }
    private string director { get; set; }

    public Movie(string name, string year, string description, string director, string runTime) : base(name, year, description)
    {
        this.director = director;
        this.runTime = runTime;
    }

    public Movie()
    {

    }

    private Dictionary<Movie, string> movies = new Dictionary<Movie, string>();

    public void moviesPage()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("***************************");
            Console.WriteLine("* " + "Movies" + " *");
            Console.WriteLine("***************************");
            Console.WriteLine();
            Console.WriteLine("1. Browse Movies");
            Console.WriteLine("2. Add Movies");
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
                        browseMovie();
                    }
                    else if (selection == "2")
                    {
                        addMovie();
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

    public void browseMovie()
    {
        load();
        displayMovies();

        while (true)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("B To Go Back");
                Console.Write("SEARCH (BLANK = View All): ");
                string search = Console.ReadLine().ToLower();
                if (search == "b") { moviesPage(); return; }

                bool movieFound = false;
                foreach (KeyValuePair<Movie, string> movie in movies)
                {
                    if (movie.Key.name.Contains(search) || movie.Value.Contains(search))
                    {
                        movieFound = true;
                        Console.WriteLine("");
                        Console.WriteLine("Movies Name:     " + movie.Value);
                        Console.WriteLine("Release Date:    " + movie.Key.year);
                        Console.WriteLine("Director:        " + movie.Key.director);
                        Console.WriteLine("Length:          " + movie.Key.runTime);
                        Console.WriteLine("Synopsis:        " + movie.Key.description);
                    }
                }

                if (!movieFound)
                {
                    Console.WriteLine("");
                    Console.Write("Movie not found....\n" +
                    "Would you like to add this Movie? (Y/N):  ");
                    string input = Console.ReadLine().ToLower();
                    if (input == "y") { addMovie(); return; }
                    else if (input == "n") { browseMovie(); return; }
                    else { Console.WriteLine("Invalid format, Y or N only"); }
                }
            }

            catch (IOException)
            {
                Console.WriteLine("Error: File read/written incorrectly");
            }
        }
    }

    public void addMovie()
    {
        save();load();
        try
        {
            Console.WriteLine("");
            Console.Write("What is the Movies name: ");
            string movieName = Console.ReadLine().ToLower();

            Console.Write("What is the Movies Date Of Registration in the Format (DD/MM/YYYY): ");
            string date = Console.ReadLine().ToLower(); dateCheck(date); ;

            Console.Write("Who is the Director: ");
            string director = Console.ReadLine().ToLower();

            Console.Write("What is the total runtime (in minutes): ");
            string runTime = Console.ReadLine().ToLower();

            Console.Write("Plot Summary: ");
            string synopsis = Console.ReadLine().ToLower();

            Movie newMovie = new Movie(name = movieName, year = date, description = synopsis, director, runTime);
            movies.Add(newMovie, movieName);
        }

        catch (IOException)
        {
            Console.WriteLine("Error: please try again later");
        }

        save(); Console.WriteLine("Movie Added Sucesfully");
        moviesPage();
    }

    void displayMovies()
    {
        foreach (KeyValuePair<Movie, string> movie in movies)
        {
            Console.WriteLine("");
            Console.WriteLine("Movies Name:     " + movie.Value);
            Console.WriteLine("Release Date:    " + movie.Key.year);
            Console.WriteLine("Director:        " + movie.Key.director);
            Console.WriteLine("Length:          " + movie.Key.runTime);
            Console.WriteLine("Synopsis:        " + movie.Key.description);
        }
    }

    void save()
    {
        try
        {
            FileStream fs = new FileStream("movies.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, movies);
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
            FileStream fs = new FileStream("movies.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            movies = bf.Deserialize(fs) as Dictionary<Movie, string>;
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