using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPlus.Models
{
    public class DetailedHall
    {
        public string Hallname { get; set; }
        public string Cinema { get; set; }
        public string Date { get; set; }
        public List<int> BusySeats { get; set; }
    }
}
