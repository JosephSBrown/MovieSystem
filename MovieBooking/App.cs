using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace MovieBooking
{
    class App
    {
        public static int movieIndex = 0;   //Sets an Index for Navigating through the Movie List
        public static List<Movie> movies = GetClassType<Movie>(); //Creates Movie Object List By Classes that have inherited from Movie

        public void Run()
        {
            Console.Title = "G&M Movie Bookings"; //Sets Console Title

            Loading();
            TitleMenu();
        }

        public static void TitleMenu()
        {
            int index = 0; //Menu Index for Navigating

            List<MenuOption> options = new List<MenuOption>()
            {
                new MenuOption("Book Tickets", () => ViewMovies()),
                new MenuOption("Exit", () => Exit())
            };

            createMenu(options, options[index]); 

            //Do-While for creating a navigatable menu using Key Presses
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow) 
                {
                    if (index - 1 >= 0)                       
                    {
                        index--;                               
                        createMenu(options, options[index]);   
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)     
                {
                    if (index + 1 < options.Count)            
                    {
                        index++;                               
                        createMenu(options, options[index]);    
                    }
                }
                else if (key.Key == ConsoleKey.Enter)          
                {
                    options[index].selected.Invoke();           
                }
            }
            while (key.Key != ConsoleKey.Escape);               
        }

        public static void createMenu(List<MenuOption> options, MenuOption selectedOption) 
        {
            Console.BackgroundColor = ConsoleColor.Black;  
            Console.ForegroundColor = ConsoleColor.White;  

            Console.Clear();

            TextDisplay.HomeScreenText();
            Console.WriteLine("");
            Console.WriteLine("");
            string info = "Use The Arrow Keys To Navigate the Menu";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (info.Length / 2)) + "}", info));
            string info2 = "Use The Enter Key To Confirm a Choice";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (info2.Length / 2)) + "}", info2));
            Console.WriteLine(" ");

            foreach (MenuOption o in options)
            { 
                if (o == selectedOption)       
                {
                    Console.BackgroundColor = ConsoleColor.White;   
                    Console.ForegroundColor = ConsoleColor.Black;  
                }
                else
                {                                                  
                    Console.BackgroundColor = ConsoleColor.Black;  
                    Console.ForegroundColor = ConsoleColor.White;  
                }
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (o.option.Length / 2)) + "}", o.option));  
                                                                                                                               
            }
        }

        public static void createMovieList(List<Movie> options, Movie selectedOption)
        {
            Console.BackgroundColor = ConsoleColor.Black;  
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            TextDisplay.MovieChoiceText();
            Console.WriteLine("");
            string info = "Press Backspace to Return to the Title Screen...";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (info.Length / 2)) + "}", info));
            Console.WriteLine("");
            Console.WriteLine("");

            foreach (Movie o in options)
            {
                if (o == selectedOption)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {                                                  
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Get(o.GetType()).Length / 2)) + "}", Get(o.GetType())));
            }
        }

        static void ViewMovies()
        {
            Console.Clear();

            createMovieList(movies, movies[movieIndex]);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor= ConsoleColor.Black;

            Console.WriteLine(" ");
            Console.WriteLine(" ");

            Console.WriteLine("Movie Title: " + movies[movieIndex].Title);
            Console.WriteLine("Movie Age Rating: " + movies[movieIndex].AgeRating);
            Console.WriteLine("Movie Audience Rating: " + movies[movieIndex].AudienceRating + "%");
            Console.WriteLine("Movie Description: " + movies[movieIndex].Description);

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();  

                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (movieIndex - 1 >= 0)           
                    {
                        movieIndex--;                  
                        ViewMovies();  
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)   
                {
                    if (movieIndex + 1 < movies.Count)            
                    {
                        movieIndex++;
                        ViewMovies();
                    }
                }
                else if (key.Key == ConsoleKey.Enter)      
                {
                    BookTickets();                                 
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    TitleMenu();
                }
            }
            while (key.Key != ConsoleKey.Escape);

        }

        static void BookTickets()
        {
            Console.Clear();
            int numRows = 10;
            int numSeats = 6;
            List<List<bool>> seatingChart= CreateSeating(numRows, numSeats);

            TextDisplay.SeatingText();
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            Console.WriteLine(movies[movieIndex].Title);

            Console.WriteLine("Current Available Seating: ");
            Console.WriteLine(" ");
            Console.WriteLine("X Indicates a Taken Seat");
            Console.WriteLine("O Indicates an Available Seat");
            Console.WriteLine(" ");
            DrawSeating(seatingChart);
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            Console.Write("How Many Tickets Would You Like To Book? ");
            string seatsRequested = Console.ReadLine();

            //Try Parse to Ensure that if the input is a letter or empty that it doesn't cause an error
            if (int.TryParse(seatsRequested, out int ticketsRequested))
            {
                Console.Clear();
                Console.WriteLine("Searching For " + ticketsRequested + " Tickets...");
                Thread.Sleep(1000);
                Console.Clear();

                List<SeatLocation> bestSeats = BestSeats(seatingChart, ticketsRequested);

                if (bestSeats.Count == ticketsRequested)
                {
                    foreach (var seat in bestSeats)
                    {
                        seatingChart[seat.Row][seat.Seat] = false;  //Reserve Seats by Making them a False Bool if there is enough Seats alongside one another
                    }

                    Console.WriteLine("Best Seats for Quantity have been Selected...");

                    Console.WriteLine(" ");
                    Console.WriteLine(" ");

                    DrawSeating(seatingChart);

                    Console.WriteLine(" ");
                    Console.WriteLine(" ");

                    Console.WriteLine("Chosen Seating:");
                    foreach (var seat in bestSeats)
                    {
                        Console.WriteLine("Row: " + (seat.Row + 1) + " Seat: " + (seat.Seat + 1));
                    }

                }
                else
                {
                    Console.WriteLine("No Avaiable Seats Together Left for this Quantity...");
                }

                Console.WriteLine(" ");
                Console.WriteLine("You'll Be Redirected Back to the Movies in 5 Seconds");
                Console.WriteLine("");

                Thread.Sleep(5000);
                ViewMovies();
            }
            else
            {
                Console.WriteLine("Invalid Input, Please Try Again");
                Thread.Sleep(3000);
                BookTickets();
            }

        }

        static List<List<bool>> CreateSeating(int numRows, int numSeats)
        {
            List<List<bool>> createChart = new List<List<bool>>(); //Create the Seating Chart in a List

            for (int rows = 0; rows < numRows; rows++)
            { 
                List<bool> rowList = new List<bool>();              //Create a List of Rows
                for (int seats = 0; seats < numSeats; seats++)
                { 
                    rowList.Add(true);              //Add All Seats as True (Available) to the List of Rows
                }
                createChart.Add(rowList);       //Add Each List of Rows for the Number of Rows to the Seating Chart
            }

            //Declare Taken Seats using Movie Method Based on Selected Movie through Index
            movies[movieIndex].takenSeats(createChart);

            return createChart;

        }

        static List<SeatLocation> BestSeats(List<List<bool>> seatingChart, int quantity)
        { 
            var bestSeats = new List<SeatLocation>();
            int numRows = seatingChart.Count;
            int numSeats = seatingChart[0].Count;

            for (int rows = numRows - 1; rows >= 0; rows--)
            {
                for (int seats = 0; seats < numSeats; seats++)
                {
                    if (ReservableSeats(seatingChart, rows, seats, quantity))
                    {
                        for (int i = 0; i < quantity; i++)
                        {
                            bestSeats.Add(new SeatLocation(rows, seats + i));

                        }
                        return bestSeats;
                    }
                }

            }
            return bestSeats;
        }

        static bool ReservableSeats(List<List<bool>> seatingChart, int row, int startSeat, int quantity)
        {
            int numSeatsPerRow = seatingChart[0].Count;
            if (startSeat + quantity > numSeatsPerRow)
            {
                return false;
            }

            for (int i = 0; i < quantity; i++)
            {
                if (!seatingChart[row][startSeat + i])
                {
                    return false;
                }
            }
            return true;
        }

        static void DrawSeating(List<List<bool>> seatingChart)
        {
            Console.WriteLine(" [SCREEN] ");
            foreach (var row in seatingChart)
            {
                foreach (var seat in row)
                {
                    char status = seat ? 'O' : 'X';     //Convert Boolean True or False to an X or O to show availability.
                    if (status == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;   //If Char is O Make Text Green to Show Availability
                        Console.Write(status + " ");
                    }
                    else if (status == 'X')
                    { 
                        Console.ForegroundColor = ConsoleColor.DarkRed;     //If Char is X Make Text Red to Show Reservation
                        Console.Write(status + " ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    
                }
                Console.ForegroundColor= ConsoleColor.White;    //Ensure beyond the Seating Chart the Console goes back to Regular Font Colour
                Console.WriteLine();
            }
        }

        //Method for getting a Class Type for Adding it to Lists
        public static List<T> GetClassType<T>() where T : class
        {
            List<T> types = new List<T>();  //Initialise New List of Classes That Are Derivatives of List Parameter Type Class
            foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                types.Add((T)Activator.CreateInstance(type));
            }
            return types;
        }

        //Method for Getting a Type and Converting it to a String
        public static string Get(Type input)
        {
            string[] words = input.ToString().Split('.');
            string lastWord = words[^1];
            string[] splitWord = Regex.Split(lastWord, @"(?<!^)(?=[A-Z])");
            string convertedWord = string.Join(" ", splitWord);
            return convertedWord;
        }


        //Loading for Start, no necessary function other than aesthetics
        static void Loading()
        {
            Console.Write("System Loading");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Clear();
        }

        //Method to Close Environment
        static void Exit()
        {
            Environment.Exit(0);
        }

    }
}
