using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPlus.Models
{
    public class Hall
    {
        public string HallName { get; set; }
        [JsonIgnore]
        public List<Movie> HallMovies { get; set; } = new List<Movie>();    
    }
}
