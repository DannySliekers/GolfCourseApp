using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfApp.Models
{
    public sealed class DisplayedBooking
    {
        public TimeOnly Time { get; set; }
        public int BookingId { get; set; }
        public int UserCount { get; set; }

        public List<Color> UserSquares
        {
            get
            {
                var squares = new List<Color>();

                for (int i = 0; i < UserCount; i++)
                {
                    squares.Add(Colors.Red);
                }
                for (int i = UserCount; i < 4; i++)
                {
                    squares.Add(Colors.Green); // available
                }

                return squares;
            }
        }
    }
}
