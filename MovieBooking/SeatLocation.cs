namespace MovieBooking
{
    class SeatLocation
    {
        //Gets the Row and Seat in the Seat Location, ready for the SeatLocation List
        public int Row { get; set; }
        public int Seat { get; set; }

        public SeatLocation(int row, int seat)
        {
            Row = row;
            Seat= seat;
        }
    }
}
