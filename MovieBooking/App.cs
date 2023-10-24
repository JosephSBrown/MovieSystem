﻿using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace MovieBooking
{
    class App
    {
        public static int movieIndex = 0;
        public static List<Movie> movies = GetClassType<Movie>();

        public void Run()
        {
            Console.Title = "G&M Movie Bookings";

            Loading();
            TitleMenu();
        }

        public static void TitleMenu()
        {
            int index = 0;

            List<MenuOption> options = new List<MenuOption>()
            {
                new MenuOption("Book Tickets", () => ViewMovies()),
                new MenuOption("Exit", () => Exit())
            };

            createMenu(options, options[index]); 

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
            int seatsRequested = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Searching For " + seatsRequested + " Tickets...");
            Thread.Sleep(1000);
            Console.Clear();

            List<SeatLocation> bestSeats = BestSeats(seatingChart, seatsRequested);

            if (bestSeats.Count == seatsRequested)
            {
                foreach (var seat in bestSeats)
                {
                    seatingChart[seat.Row][seat.Seat] = false;
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
                Console.WriteLine("No Avaiable Seats Left for Quantity...");
            }
        }

        static List<List<bool>> CreateSeating(int numRows, int numSeats)
        {
            List<List<bool>> createChart = new List<List<bool>>();

            for (int rows = 0; rows < numRows; rows++)
            { 
                List<bool> rowList = new List<bool>();
                for (int seats = 0; seats < numSeats; seats++)
                { 
                    rowList.Add(true);
                }
                createChart.Add(rowList);
            }

            //Declare Taken Seats
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
                    char status = seat ? 'O' : 'X';
                    if (status == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(status + " ");
                    }
                    else if (status == 'X')
                    { 
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(status + " ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    
                }
                Console.ForegroundColor= ConsoleColor.White;
                Console.WriteLine();
            }
        }

        //Method for getting a Class Type for Adding it to Lists
        public static List<T> GetClassType<T>() where T : class
        {
            List<T> types = new List<T>();  //Initialise New List of Classes That Are Derivatives of List Parameter T
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

        static void Exit()
        {
            Environment.Exit(0);
        }

    }
}
