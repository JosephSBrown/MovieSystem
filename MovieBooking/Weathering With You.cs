namespace MovieBooking
{
    class WeatheringWithYou : Movie
    {
        public WeatheringWithYou()
        {
            Title = "Weathering With You";
            AgeRating = 13;
            AudienceRating = 93;
            Description = "In Tokyo, a runaway high school student facing financial struggles ends up with a job at a small-time publisher. One day, he meets a young girl who has the ability to control the weather.";
        }

        public override void takenSeats(List<List<bool>> chart)
        {
            chart[9][0] = false;
            chart[9][1] = false;
            chart[9][2] = false;
            chart[9][3] = false;
            chart[9][4] = false;
            chart[8][5] = false;
            chart[8][4] = false;
            chart[8][3] = false;
            chart[8][2] = false;
            chart[7][4] = false;
            chart[7][1] = false;
            chart[7][0] = false;
            chart[6][4] = false;
            chart[6][3] = false;
            chart[5][1] = false;
            chart[5][0] = false;
            chart[4][5] = false;
            chart[4][4] = false;
            chart[4][3] = false;
        }
    }
}
