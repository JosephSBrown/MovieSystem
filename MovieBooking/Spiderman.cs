namespace MovieBooking
{
    class Spiderman : Movie
    {
        //Spiderman Object
        public Spiderman()
        {
            Title = "Spiderman: Far From Home";
            AgeRating = 12;
            AudienceRating = 83;
            Description = "Peter Parker, the beloved superhero Spider-Man, faces four destructive elemental monsters while on holiday in Europe. Soon, he receives help from Mysterio, a fellow hero with mysterious origins.";
        }

        public override void takenSeats(List<List<bool>> chart)
        {
            chart[1][4] = false;
            chart[9][0] = false;
            chart[9][1] = false;
            chart[9][2] = false;
        }
    }
}
