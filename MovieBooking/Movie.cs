namespace MovieBooking
{
    class Movie : IMovie
    {
        //Movie Object
        public string Title { get; set; }
        public int AgeRating { get; set; }
        public int AudienceRating { get; set; }
        public string Description { get; set; }

        public virtual void takenSeats(List<List<bool>> chart)
        { 
            
        }

    }
}
