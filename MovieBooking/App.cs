using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking
{
    class App
    {
        public void Run()
        {
            Loading();
            BookTickets();
        }

        static void BookTickets()
        {
            int numRows = 10;
            int numSeats = 6;
            List<List<bool>> seatingChart= CreateSeating(numRows, numSeats);

            Console.WriteLine("Current Available Seating: ");
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
            FastAndFurious fnf = new FastAndFurious();
            fnf.takenSeats(createChart);
            //Movies[index].takenSeats(createChart);

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
            foreach (var row in seatingChart)
            {
                foreach (var seat in row)
                {
                    char status = seat ? 'O' : 'X';
                    Console.Write(status + " ");
                }
                Console.WriteLine();
            }
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


    }
}
