namespace MovieBooking
{
    internal interface IMovie
    {
        void takenSeats(List<List<bool>> chart); //Method Interfaced for Movie, which will be inherited by Movie Class Types
    }
}
