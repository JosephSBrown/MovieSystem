using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking
{
    class Movie : IMovie
    {
        public string Title { get; set; }
        public int AgeRating { get; set; }
        public int AudienceRating { get; set; }
        public string Description { get; set; }

        public virtual void takenSeats(List<List<bool>> chart)
        { 
            
        }

    }
}
