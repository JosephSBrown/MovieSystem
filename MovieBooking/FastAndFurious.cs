namespace MovieBooking
{
    class FastAndFurious : Movie
    {
        public FastAndFurious()
        {
            Title = "Fast And Furious: Tokyo Drift";
            AgeRating = 13;
            AudienceRating = 84;
            Description = "In order to avoid jail time, an errant car racer is sent to live with his father in Tokyo. However, he finds himself in trouble once again when he becomes a major competitor in drifting.";
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
