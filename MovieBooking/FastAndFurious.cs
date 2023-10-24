using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking
{
    class FastAndFurious : Movie
    {
        public FastAndFurious()
        {
            Title = "Fast And Furious: Tokyo Drift";
            AgeRating = 15;
            AudienceRating = 84;
            Description = "";
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
