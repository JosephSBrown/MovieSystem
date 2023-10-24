namespace MovieBooking
{
    class SeatLocation
    {
        public int Row { get; set; }
        public int Seat { get; set; }

        public SeatLocation(int row, int seat)
        {
            Row = row;
            Seat= seat;
        }
    }
}
