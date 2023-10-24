namespace MovieBooking
{
    class Blackberry : Movie
    {
        public Blackberry()
        {
            Title = "Blackberry";
            AgeRating = 15;
            AudienceRating = 88;
            Description = "Explores the incredible growth and tragic collapse of the world's first smartphone and how it smashed huge enterprises before surrendering to Silicon Valley's fiercely competitive companies.";
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
            chart[7][5] = false;
            chart[7][4] = false;
            chart[7][2] = false;
            chart[7][0] = false;
            chart[6][5] = false;
            chart[6][3] = false;
            chart[6][2] = false;
            chart[5][0] = false;
            chart[5][1] = false;
            chart[5][2] = false;
            chart[5][3] = false;
            chart[5][4] = false;
            chart[4][5] = false;
            chart[4][4] = false;
            chart[4][3] = false;
            chart[3][5] = false;
            chart[3][4] = false;
            chart[3][2] = false;
            chart[3][0] = false;
            chart[2][5] = false;
            chart[2][3] = false;
            chart[2][2] = false;
        }
    }
}
